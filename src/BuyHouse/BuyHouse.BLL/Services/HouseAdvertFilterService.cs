using BuyHouse.BLL.Clients;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class HouseAdvertFilterService : IHouseAdvertFilterService
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrencyConverterClient _clientCurrencyConverter;
        public HouseAdvertFilterService(ApplicationDbContext context, CurrencyConverterClient clientCurrencyConverter)
        {
            _context = context;
            _clientCurrencyConverter = clientCurrencyConverter;
        }

        /// <summary>
        /// Filter house adverts
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>filtered house adverts</returns>
        public async Task<ResponseHouseAdvertDTO> GetHouseAdvertByParametersAsync(HouseAdvertFilter filter, int pageSize, int page = 1)
        {
            if (pageSize == 0)
                pageSize = 10;

            IQueryable<HouseAdvert> houseAdverts = _context.HouseAdverts;

            if (!String.IsNullOrEmpty(filter.CityName))
            {
                houseAdverts = houseAdverts.Where(p => p.MainInfo.City.Contains(filter.CityName));
            }

            if (filter.CountRooms != null)
            {
                switch (filter.CountRooms)
                {
                    case "All":
                        houseAdverts = houseAdverts.Where(p => p.Rooms >= 0);
                        break;
                    case "1":
                    case "2":
                    case "3":
                        houseAdverts = houseAdverts.Where(p => p.Rooms.ToString() == filter.CountRooms);
                        break;
                    case "4+":
                        houseAdverts = houseAdverts.Where(p => p.Rooms >= 4);
                        break;
                }
            }

            Task<ulong> minPriceFormUSDToUAH, minPriceFormUSDToEUR, minPriceFormEURToUSD, minPriceFormEURToUAH, minPriceFormUAHToUSD, minPriceFormUAHToEUR;
            Task<ulong> maxPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormEURToUSD, maxPriceFormEURToUAH, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR;

            if (filter.Currency != null)
            {
                houseAdverts = houseAdverts.Where(p => p.Currency != null);
                IQueryable<HouseAdvert> houseAdvertEUR = houseAdverts.Where(p => p.Currency == Currency.Euro);
                IQueryable<HouseAdvert> houseAdvertUAH = houseAdverts.Where(p => p.Currency == Currency.UAH);
                IQueryable<HouseAdvert> houseAdvertUSD = houseAdverts.Where(p => p.Currency == Currency.USD);
                switch (filter.Currency.ToString())
                {

                    case "Any":
                        houseAdverts = await FilterByPriceAsync(houseAdverts, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);
                        break;
                    case "USD":
                        minPriceFormUSDToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MinPrice);
                        minPriceFormUSDToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MinPrice);
                        maxPriceFormUSDToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MaxPrice);
                        maxPriceFormUSDToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUSDToEUR, minPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormUSDToUAH);

                        houseAdvertEUR = await FilterByPriceAsync(houseAdvertEUR, filter.TypeOfPrice, minPriceFormUSDToEUR.Result, maxPriceFormUSDToEUR.Result);
                        houseAdvertUAH = await FilterByPriceAsync(houseAdvertUAH, filter.TypeOfPrice, minPriceFormUSDToUAH.Result, maxPriceFormUSDToUAH.Result);
                        houseAdvertUSD = await FilterByPriceAsync(houseAdvertUSD, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        houseAdverts = houseAdvertEUR.Union(houseAdvertUAH).Union(houseAdvertUSD);
                        break;
                    case "Euro":
                        minPriceFormEURToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MinPrice);
                        minPriceFormEURToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MinPrice);
                        maxPriceFormEURToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MaxPrice);
                        maxPriceFormEURToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormEURToUSD, minPriceFormEURToUAH, maxPriceFormEURToUSD, maxPriceFormEURToUAH);

                        houseAdvertUSD = await FilterByPriceAsync(houseAdvertUSD, filter.TypeOfPrice, minPriceFormEURToUSD.Result, maxPriceFormEURToUSD.Result);
                        houseAdvertUAH = await FilterByPriceAsync(houseAdvertUAH, filter.TypeOfPrice, minPriceFormEURToUAH.Result, maxPriceFormEURToUAH.Result);
                        houseAdvertEUR = await FilterByPriceAsync(houseAdvertEUR, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        houseAdverts = houseAdvertEUR.Union(houseAdvertUAH).Union(houseAdvertUSD);
                        break;
                    case "UAH":

                        minPriceFormUAHToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MinPrice);
                        minPriceFormUAHToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MinPrice);
                        maxPriceFormUAHToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MaxPrice);
                        maxPriceFormUAHToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUAHToUSD, minPriceFormUAHToEUR, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR);

                        houseAdvertUSD = await FilterByPriceAsync(houseAdvertUSD, filter.TypeOfPrice, minPriceFormUAHToUSD.Result, maxPriceFormUAHToUSD.Result);
                        houseAdvertEUR = await FilterByPriceAsync(houseAdvertEUR, filter.TypeOfPrice, minPriceFormUAHToEUR.Result, maxPriceFormUAHToEUR.Result);
                        houseAdvertUAH = await FilterByPriceAsync(houseAdvertUAH, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        houseAdverts = houseAdvertEUR.Union(houseAdvertUAH).Union(houseAdvertUSD);
                        break;
                }
            }

            if (filter.MaxTotalArea != null && filter.MinTotalArea != null)
            {
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea == 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalArea != null);
                if (filter.MinTotalArea != 0 && filter.MaxTotalArea == 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalArea >= filter.MinTotalArea);
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea != 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalArea <= filter.MinTotalArea);
                if (filter.MinTotalArea > 0 && filter.MaxTotalArea > 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalArea >= filter.MinTotalArea && p.TotalArea <= filter.MaxTotalArea);
            }

            if (filter.MaxLandArea != null && filter.MinLandArea != null)
            {
                if (filter.MinLandArea == 0 && filter.MaxLandArea == 0)
                    houseAdverts = houseAdverts.Where(p => p.LandArea != null);
                if (filter.MinLandArea != 0 && filter.MaxLandArea == 0)
                    houseAdverts = houseAdverts.Where(p => p.LandArea >= filter.MinTotalArea);
                if (filter.MinLandArea == 0 && filter.MaxLandArea != 0)
                    houseAdverts = houseAdverts.Where(p => p.LandArea <= filter.MinTotalArea);
                if (filter.MinLandArea > 0 && filter.MaxLandArea > 0)
                    houseAdverts = houseAdverts.Where(p => p.LandArea >= filter.MinLandArea && p.LandArea <= filter.MaxLandArea);
            }

            var count = await houseAdverts.CountAsync();
            var houseAdverts_ = await houseAdverts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ResponseHouseAdvertDTO output = new ResponseHouseAdvertDTO()
            {
                Count = count,
                HouseAdverts = houseAdverts_,
                PageSize = pageSize
            };

            return output;
        }

        /// <summary>
        /// Filter house advers by min and max price
        /// </summary>
        /// <param name="flatAdverts"></param>
        /// <param name="typeOfPrice"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns>query house adverts</returns>
        private async Task<IQueryable<HouseAdvert>> FilterByPriceAsync(IQueryable<HouseAdvert> houseAdverts, TypeOfPrice typeOfPrice, ulong minPrice, ulong maxPrice)
        {
            if (typeOfPrice == TypeOfPrice.TotalPrice)
            {
                if (minPrice == 0 && maxPrice == 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalPrice != null);
                if (minPrice != 0 && maxPrice == 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalPrice >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalPrice <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    houseAdverts = houseAdverts.Where(p => p.TotalPrice >= minPrice && p.TotalPrice <= maxPrice);

                return houseAdverts;
            }
            else
            {
                if (minPrice == 0 && maxPrice == 0)
                    houseAdverts = houseAdverts.Where(p => p.PricePerSquareMeter != null);
                if (minPrice != 0 && maxPrice == 0)
                    houseAdverts = houseAdverts.Where(p => p.PricePerSquareMeter >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    houseAdverts = houseAdverts.Where(p => p.PricePerSquareMeter <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    houseAdverts = houseAdverts.Where(p => p.PricePerSquareMeter >= minPrice && p.PricePerSquareMeter <= maxPrice);

                return houseAdverts;
            }
        }

        /// <summary>
        /// Get most liked house advert
        /// </summary>
        /// <returns>list of most liked flat advert</returns>
        public async Task<IEnumerable<HouseAdvert>> GetMostLikedHouseAdvertAsync()
        {
            const int numberOfAdvert = 3;
            IEnumerable<HouseAdvert> houseAdverts = await _context.HouseAdverts.OrderByDescending(p => p.LikeCount).Take(numberOfAdvert).ToListAsync();
            return houseAdverts;
        }

        /// <summary>
        /// Get seller`s house adverts
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>List of house adverts</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<HouseAdvert>> GetSellersHouseAdvertsAsync(string? currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
                throw new Exception("Current user Id can not be null or empty");

            IEnumerable<HouseAdvert> houseAdverts = await _context.HouseAdverts.Where(x => x.UserID == currentUserId).ToListAsync();
            return houseAdverts;
        }
    }
}
