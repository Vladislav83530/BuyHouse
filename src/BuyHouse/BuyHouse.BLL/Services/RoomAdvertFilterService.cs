using BuyHouse.BLL.Clients;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class RoomAdvertFilterService : IAdvertFilterService<RoomAdvert, RoomAdvertFilter>
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrencyConverterClient _clientCurrencyConverter;
        public RoomAdvertFilterService(ApplicationDbContext context, CurrencyConverterClient clientCurrencyConverter)
        {
            _context = context;
            _clientCurrencyConverter = clientCurrencyConverter;
        }

        /// <summary>
        /// Filter room adverts
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>filtered room adverts</returns>
        public async Task<ResponseAdvertDTO<RoomAdvert>> GetAdvertByParametersAsync(RoomAdvertFilter filter, int pageSize, int page = 1)
        {
            if (pageSize == 0)
                pageSize = 10;

            IQueryable<RoomAdvert> roomAdverts = _context.RoomAdverts;

            if (!String.IsNullOrEmpty(filter.CityName))
            {
                roomAdverts = roomAdverts.Where(p => p.MainInfo.City.Contains(filter.CityName));
            }

            Task<ulong> minPriceFormUSDToUAH, minPriceFormUSDToEUR, minPriceFormEURToUSD, minPriceFormEURToUAH, minPriceFormUAHToUSD, minPriceFormUAHToEUR;
            Task<ulong> maxPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormEURToUSD, maxPriceFormEURToUAH, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR;

            if (filter.Currency != null)
            {
                roomAdverts = roomAdverts.Where(p => p.Currency != null);
                IQueryable<RoomAdvert> roomAdvertEUR = roomAdverts.Where(p => p.Currency == Currency.Euro);
                IQueryable<RoomAdvert> roomAdvertUAH = roomAdverts.Where(p => p.Currency == Currency.UAH);
                IQueryable<RoomAdvert> roomAdvertUSD = roomAdverts.Where(p => p.Currency == Currency.USD);
                switch (filter.Currency.ToString())
                {

                    case "Any":
                        roomAdverts = await FilterByPriceAsync(roomAdverts, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);
                        break;
                    case "USD":
                        minPriceFormUSDToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MinPrice);
                        minPriceFormUSDToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MinPrice);
                        maxPriceFormUSDToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "EUR", filter.MaxPrice);
                        maxPriceFormUSDToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("USD", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUSDToEUR, minPriceFormUSDToUAH, maxPriceFormUSDToEUR, maxPriceFormUSDToUAH);

                        roomAdvertEUR = await FilterByPriceAsync(roomAdvertEUR, filter.TypeOfPrice, minPriceFormUSDToEUR.Result, maxPriceFormUSDToEUR.Result);
                        roomAdvertUAH = await FilterByPriceAsync(roomAdvertUAH, filter.TypeOfPrice, minPriceFormUSDToUAH.Result, maxPriceFormUSDToUAH.Result);
                        roomAdvertUSD = await FilterByPriceAsync(roomAdvertUSD, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        roomAdverts = roomAdvertEUR.Union(roomAdvertUAH).Union(roomAdvertUSD);
                        break;
                    case "Euro":
                        minPriceFormEURToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MinPrice);
                        minPriceFormEURToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MinPrice);
                        maxPriceFormEURToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "USD", filter.MaxPrice);
                        maxPriceFormEURToUAH = _clientCurrencyConverter.ConvertCurrecyAsync("EUR", "UAH", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormEURToUSD, minPriceFormEURToUAH, maxPriceFormEURToUSD, maxPriceFormEURToUAH);

                        roomAdvertUSD = await FilterByPriceAsync(roomAdvertUSD, filter.TypeOfPrice, minPriceFormEURToUSD.Result, maxPriceFormEURToUSD.Result);
                        roomAdvertUAH = await FilterByPriceAsync(roomAdvertUAH, filter.TypeOfPrice, minPriceFormEURToUAH.Result, maxPriceFormEURToUAH.Result);
                        roomAdvertEUR = await FilterByPriceAsync(roomAdvertEUR, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        roomAdverts = roomAdvertEUR.Union(roomAdvertUAH).Union(roomAdvertUSD);
                        break;
                    case "UAH":

                        minPriceFormUAHToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MinPrice);
                        minPriceFormUAHToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MinPrice);
                        maxPriceFormUAHToUSD = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "USD", filter.MaxPrice);
                        maxPriceFormUAHToEUR = _clientCurrencyConverter.ConvertCurrecyAsync("UAH", "EUR", filter.MaxPrice);
                        await Task.WhenAll(minPriceFormUAHToUSD, minPriceFormUAHToEUR, maxPriceFormUAHToUSD, maxPriceFormUAHToEUR);

                        roomAdvertUSD = await FilterByPriceAsync(roomAdvertUSD, filter.TypeOfPrice, minPriceFormUAHToUSD.Result, maxPriceFormUAHToUSD.Result);
                        roomAdvertEUR = await FilterByPriceAsync(roomAdvertEUR, filter.TypeOfPrice, minPriceFormUAHToEUR.Result, maxPriceFormUAHToEUR.Result);
                        roomAdvertUAH = await FilterByPriceAsync(roomAdvertUAH, filter.TypeOfPrice, filter.MinPrice, filter.MaxPrice);

                        roomAdverts = roomAdvertEUR.Union(roomAdvertUAH).Union(roomAdvertUSD);
                        break;
                }
            }

            if (filter.MaxTotalArea != null && filter.MinTotalArea != null)
            {
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea == 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalArea != null);
                if (filter.MinTotalArea != 0 && filter.MaxTotalArea == 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalArea >= filter.MinTotalArea);
                if (filter.MinTotalArea == 0 && filter.MaxTotalArea != 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalArea <= filter.MinTotalArea);
                if (filter.MinTotalArea > 0 && filter.MaxTotalArea > 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalArea >= filter.MinTotalArea && p.TotalArea <= filter.MaxTotalArea);
            }

            if (filter.MaxFloor != null && filter.MinFloor != null)
            {
                if (filter.MinFloor == 0 && filter.MaxFloor == 0)
                    roomAdverts = roomAdverts.Where(p => p.Floor != null);
                if (filter.MinFloor != 0 && filter.MaxFloor == 0)
                    roomAdverts = roomAdverts.Where(p => p.Floor >= filter.MinFloor);
                if (filter.MinFloor == 0 && filter.MaxFloor != 0)
                    roomAdverts = roomAdverts.Where(p => p.Floor <= filter.MinFloor);
                if (filter.MinFloor > 0 && filter.MaxFloor > 0)
                    roomAdverts = roomAdverts.Where(p => p.Floor >= filter.MinFloor && p.Floor <= filter.MaxFloor);
            }

            var count = await roomAdverts.CountAsync();
            var roomAdverts_ = await roomAdverts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ResponseAdvertDTO<RoomAdvert> output = new ResponseAdvertDTO<RoomAdvert>()
            {
                Count = count,
                Adverts = roomAdverts_,
                PageSize = pageSize
            };

            return output;
        }

        /// <summary>
        /// Get most liked room advert
        /// </summary>
        /// <returns>list of most liked room advert</returns>
        public async Task<IEnumerable<RoomAdvert>> GetMostLikedAdvertAsync()
        {
            const int numberOfAdvert = 3;
            IEnumerable<RoomAdvert> roomAdverts = await _context.RoomAdverts.OrderByDescending(p => p.LikeCount).Take(numberOfAdvert).ToListAsync();
            return roomAdverts;
        }

        /// <summary>
        /// Get seller`s room adverts
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>List of room adverts</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<RoomAdvert>> GetSellersAdvertsAsync(string? currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
                throw new Exception("Current user Id can not be null or empty");

            IEnumerable<RoomAdvert> roomAdverts = await _context.RoomAdverts.Where(x => x.UserID == currentUserId).ToListAsync();
            return roomAdverts;
        }


        /// <summary>
        /// Filter room advers by min and max price
        /// </summary>
        /// <param name="roomAdverts"></param>
        /// <param name="typeOfPrice"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns>query room adverts</returns>
        private async Task<IQueryable<RoomAdvert>> FilterByPriceAsync(IQueryable<RoomAdvert> roomAdverts, TypeOfPrice typeOfPrice, ulong minPrice, ulong maxPrice)
        {
            if (typeOfPrice == TypeOfPrice.TotalPrice)
            {
                if (minPrice == 0 && maxPrice == 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalPrice != null);
                if (minPrice != 0 && maxPrice == 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalPrice >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalPrice <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    roomAdverts = roomAdverts.Where(p => p.TotalPrice >= minPrice && p.TotalPrice <= maxPrice);

                return roomAdverts;
            }
            else
            {
                if (minPrice == 0 && maxPrice == 0)
                    roomAdverts = roomAdverts.Where(p => p.PricePerSquareMeter != null);
                if (minPrice != 0 && maxPrice == 0)
                    roomAdverts = roomAdverts.Where(p => p.PricePerSquareMeter >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    roomAdverts = roomAdverts.Where(p => p.PricePerSquareMeter <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    roomAdverts = roomAdverts.Where(p => p.PricePerSquareMeter >= minPrice && p.PricePerSquareMeter <= maxPrice);

                return roomAdverts;
            }
        }
    }
}

