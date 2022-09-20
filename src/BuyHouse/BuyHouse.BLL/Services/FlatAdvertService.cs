using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertService : IFlatAdvertService
    {
        private readonly ApplicationDbContext _context;
        public FlatAdvertService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseFlatAdvertDTO> GetFlatAdvertByParameters( FlatAdvertFilter filter, int pageSize, int page = 1)
        {
            if (pageSize == 0)
                pageSize = 3;

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

            if (filter.MaxPrice != null && filter.MinPrice != null)
            {

                if (filter.TypeOfPrice == TypeOfPrice.TotalPrice)
                {
                    if (filter.MinPrice == 0 && filter.MaxPrice == 0)
                        flatAdverts = flatAdverts.Where(p => p.TotalPrice != null);
                    if (filter.MinPrice != 0 && filter.MaxPrice == 0)
                        flatAdverts = flatAdverts.Where(p => p.TotalPrice >= filter.MinPrice);
                    if (filter.MinPrice == 0 && filter.MaxPrice != 0)
                        flatAdverts = flatAdverts.Where(p => p.TotalPrice <= filter.MaxPrice);
                    if (filter.MinPrice > 0 && filter.MaxPrice > 0)
                        flatAdverts = flatAdverts.Where(p => p.TotalPrice >= filter.MinPrice && p.TotalPrice <= filter.MaxPrice);
                }
                else
                {
                    if (filter.MinPrice == 0 && filter.MaxPrice == 0)
                        flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter != null);
                    if (filter.MinPrice != 0 && filter.MaxPrice == 0)
                        flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter >= filter.MinPrice);
                    if (filter.MinPrice == 0 && filter.MaxPrice != 0)
                        flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter <= filter.MaxPrice);
                    if (filter.MinPrice > 0 && filter.MaxPrice > 0)
                        flatAdverts = flatAdverts.Where(p => p.PricePerSquareMeter >= filter.MinPrice && p.PricePerSquareMeter <= filter.MaxPrice);
                }
            }

            if (filter.Currency!=null)
            {
                switch (filter.Currency.ToString())
                {
                    case "Any":
                        flatAdverts = flatAdverts.Where(p => p.Currency != null);
                        break;
                    case "USD":
                        flatAdverts = flatAdverts.Where(p => p.Currency == Currency.USD);
                        break;
                    case "Euro":
                        flatAdverts = flatAdverts.Where(p => p.Currency == Currency.Euro);
                        break;
                    case "UAH":
                        flatAdverts = flatAdverts.Where(p => p.Currency == Currency.UAH);
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

            ResponseFlatAdvertDTO output = new ResponseFlatAdvertDTO()
            {
                Count = count,
                FlatAdverts = flatAdverts_,
                PageSize = pageSize
            };

            return output;
        }

        public async Task<IEnumerable<FlatAdvert>> GetMostLikedFlatAdvert()
        {
            IEnumerable<FlatAdvert> flatAdverts = await _context.FlatAdverts.OrderBy(p => p.LikeCount).Take(3).ToListAsync();
            return flatAdverts;
        }
    }
}
