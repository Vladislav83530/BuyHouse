using BuyHouse.BLL.Clients;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertFilterService : IAdvertFilterService<FlatAdvert, FlatAdvertFilter>
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrencyConverterClient _clientCurrencyConverter;
        public FlatAdvertFilterService(ApplicationDbContext context, CurrencyConverterClient clientCurrencyConverter)
        {
            _context = context;
            _clientCurrencyConverter = clientCurrencyConverter;
        }

        /// <summary>
        /// Filter falt adverts
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>filtered flat adverts</returns>
        public async Task<ResponseAdvertDTO<FlatAdvert>> GetAdvertByParametersAsync(FlatAdvertFilter filter, int pageSize, int page = 1)
        {
            if (pageSize == 0)
                pageSize = 10;

            IQueryable<FlatAdvert> flatAdverts = _context.FlatAdverts;

            if (!String.IsNullOrEmpty(filter.CityName))
            {
                flatAdverts = flatAdverts.Where(p => p.MainInfo.City.Contains(filter.CityName));
            }

            if (filter.CountRooms != null)
            {
                switch (filter.CountRooms)
                {
                    case "All":
                        flatAdverts = flatAdverts.Where(p => p.Rooms >= 0);
                        break;
                    case "1":
                    case "2":
                    case "3":
                        flatAdverts = flatAdverts.Where(p => p.Rooms.ToString() == filter.CountRooms);
                        break;
                    case "4+":
                        flatAdverts = flatAdverts.Where(p => p.Rooms >= 4);
                        break;
                }
            }

            Task<ulong> minPriceFormUSDToUAH, minPriceFormUSDToEUR, minPriceFormEURToUSD, minPriceFormEURToUAH, minPriceFormUAHToUSD, minPriceFormUAHToEUR;
            Task<ulong> maxPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormEURToUSD, maxPriceFormEURToUAH, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR;

            if (filter.Currency != null)
            {
                flatAdverts = flatAdverts.Where(p => p.Currency != null);
                IQueryable<FlatAdvert> flatAdvertEUR = flatAdverts.Where(p => p.Currency == Currency.Euro);
                IQueryable<FlatAdvert> flatAdvertUAH = flatAdverts.Where(p => p.Currency == Currency.UAH);
                IQueryable<FlatAdvert> flatAdvertUSD = flatAdverts.Where(p => p.Currency == Currency.USD);
                switch (filter.Currency.ToString())
                {

                    case "Any":
                        flatAdverts = await FilterByPriceAsync(flatAdverts, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);
                        break;
                    case "USD":
                        minPriceFormUSDToEUR =  _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MinPrice);
                        minPriceFormUSDToUAH =  _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MinPrice);
                        maxPriceFormUSDToEUR =  _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MaxPrice);
                        maxPriceFormUSDToUAH =  _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUSDToEUR, minPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormUSDToUAH);

                        flatAdvertEUR = await FilterByPriceAsync(flatAdvertEUR, filter.TypeOfPrice, minPriceFormUSDToEUR.Result, maxPriceFormUSDToEUR.Result);
                        flatAdvertUAH = await FilterByPriceAsync(flatAdvertUAH, filter.TypeOfPrice, minPriceFormUSDToUAH.Result, maxPriceFormUSDToUAH.Result);
                        flatAdvertUSD = await FilterByPriceAsync(flatAdvertUSD, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        flatAdverts = flatAdvertEUR.Union(flatAdvertUAH).Union(flatAdvertUSD);
                        break;
                    case "Euro":
                        minPriceFormEURToUSD =  _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MinPrice);
                        minPriceFormEURToUAH =  _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MinPrice);
                        maxPriceFormEURToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MaxPrice);
                        maxPriceFormEURToUAH =  _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormEURToUSD, minPriceFormEURToUAH, maxPriceFormEURToUSD, maxPriceFormEURToUAH);

                        flatAdvertUSD = await FilterByPriceAsync(flatAdvertUSD, filter.TypeOfPrice, minPriceFormEURToUSD.Result, maxPriceFormEURToUSD.Result);
                        flatAdvertUAH = await FilterByPriceAsync(flatAdvertUAH, filter.TypeOfPrice, minPriceFormEURToUAH.Result, maxPriceFormEURToUAH.Result);
                        flatAdvertEUR = await FilterByPriceAsync(flatAdvertEUR, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        flatAdverts = flatAdvertEUR.Union(flatAdvertUAH).Union(flatAdvertUSD);
                        break;
                    case "UAH":

                        minPriceFormUAHToUSD =  _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MinPrice);
                        minPriceFormUAHToEUR =  _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MinPrice);
                        maxPriceFormUAHToUSD =  _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MaxPrice);
                        maxPriceFormUAHToEUR =  _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUAHToUSD, minPriceFormUAHToEUR, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR);

                        flatAdvertUSD = await FilterByPriceAsync(flatAdvertUSD, filter.TypeOfPrice, minPriceFormUAHToUSD.Result, maxPriceFormUAHToUSD.Result);
                        flatAdvertEUR = await FilterByPriceAsync(flatAdvertEUR, filter.TypeOfPrice, minPriceFormUAHToEUR.Result, maxPriceFormUAHToEUR.Result);
                        flatAdvertUAH = await FilterByPriceAsync(flatAdvertUAH, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        flatAdverts = flatAdvertEUR.Union(flatAdvertUAH).Union(flatAdvertUSD);
                        break;
                }
            }

            if (filter.MaxTotalArea != null && filter.MinTotalArea != null)
            {
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea == 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea != null);
                if (filter.MinTotalArea != 0 && filter.MaxTotalArea == 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea >= filter.MinTotalArea);
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea != 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea <= filter.MinTotalArea);
                if (filter.MinTotalArea > 0 && filter.MaxTotalArea > 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea >= filter.MinTotalArea && p.TotalArea <= filter.MaxTotalArea);
            }

            if (filter.MaxFloor != null && filter.MinFloor != null)
            {
                if (filter.MinFloor == 0 && filter.MaxFloor == 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor != null);
                if (filter.MinFloor != 0 && filter.MaxFloor == 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor >= filter.MinFloor);
                if (filter.MinFloor == 0 && filter.MaxFloor != 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor <= filter.MinFloor);
                if (filter.MinFloor > 0 && filter.MaxFloor > 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor >= filter.MinFloor && p.Floor <= filter.MaxFloor);
            }

            var count = await flatAdverts.CountAsync();
            var flatAdverts_ = await flatAdverts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ResponseAdvertDTO<FlatAdvert> output = new ResponseAdvertDTO<FlatAdvert>()
            {
                Count = count,
                Adverts = flatAdverts_,
                PageSize = pageSize
            };

            return output;
        }

        /// <summary>
        /// Get most liked flat advert
        /// </summary>
        /// <returns>list of most liked flat advert</returns>
        public async Task<IEnumerable<FlatAdvert>> GetMostLikedAdvertAsync()
        {
            const int numberOfAdvert = 3;
            IEnumerable<FlatAdvert> flatAdverts = await _context.FlatAdverts.OrderByDescending(p => p.LikeCount).Take(numberOfAdvert).ToListAsync();
            return flatAdverts;
        }

        /// <summary>
        /// Get seller`s flat adverts
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>List of flat adverts</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<FlatAdvert>> GetSellersAdvertsAsync(string? currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
                throw new Exception("Current user Id can not be null or empty");

            IEnumerable<FlatAdvert> flatAdverts = await _context.FlatAdverts.Where(x => x.UserID == currentUserId).ToListAsync();
            return flatAdverts;
        }


        /// <summary>
        /// Filter flat advers by min and max price
        /// </summary>
        /// <param name="flatAdverts"></param>
        /// <param name="typeOfPrice"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns>query flat adverts</returns>
        private async Task<IQueryable<FlatAdvert>> FilterByPriceAsync(IQueryable<FlatAdvert> flatAdverts, TypeOfPrice typeOfPrice, ulong minPrice, ulong maxPrice)
        {
            if (typeOfPrice == TypeOfPrice.TotalPrice)
            {
                if (minPrice == 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalPrice != null);
                if (minPrice != 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalPrice >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalPrice <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalPrice >= minPrice && p.TotalPrice <= maxPrice);

                return flatAdverts;
            }
            else
            {
                if (minPrice == 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter != null);
                if (minPrice != 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter >= minPrice && p.PricePerSquareMeter <= maxPrice);

                return flatAdverts;
            }
        }
    }
}