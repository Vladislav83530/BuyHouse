using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertService : AdvertService<FlatAdvert>, IFlatAdvertService
    {
        private readonly ApplicationDbContext _context;
        public FlatAdvertService(ApplicationDbContext context, IPhotosService photoService) : base(context, photoService)
        {
            _context = context;
        }

        public async Task<ResponseFlatAdvertDTO> GetFlatAdvertByParameters(
            string cityName, 
            string countRooms, 
            int minPrice, int maxPrice, 
            string currency, 
            string typeOfPrice, 
            double minTotalArea, double maxTotalArea, 
            int minFloor, int maxFloor, 
            int page = 1)
        {
            int pageSize = 3;

            IQueryable<FlatAdvert> flatAdverts = _context.FlatAdverts;

            if (!String.IsNullOrEmpty(cityName))
            {
                flatAdverts = flatAdverts.Where(p => p.MainInfo.City.Contains(cityName));
            }

            if (countRooms != null)
            {
                switch (countRooms)
                {
                    case " ":
                        flatAdverts = flatAdverts.Where(p => p.Rooms >= 0);
                        break;
                    case "1":
                    case "2":
                    case "3":
                        flatAdverts = flatAdverts.Where(p => p.Rooms.ToString() == countRooms);
                        break;
                    case "4+":
                        flatAdverts = flatAdverts.Where(p => p.Rooms >= 4);
                        break;
                }
            }

            if (maxPrice != null && minPrice != null)
            {
                if (minPrice == 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.Price != null);
                if (minPrice != 0 && maxPrice == 0)
                    flatAdverts = flatAdverts.Where(p => p.Price >= minPrice);
                if (minPrice == 0 && maxPrice != 0)
                    flatAdverts = flatAdverts.Where(p => p.Price <= maxPrice);
                if (minPrice > 0 && maxPrice > 0)
                    flatAdverts = flatAdverts.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }


            if (!String.IsNullOrEmpty(currency))
            {
                switch (currency)
                {
                    case "Будь-яка":
                        flatAdverts = flatAdverts.Where(p => p.Currency != null);
                        break;
                    case "$":
                        flatAdverts = flatAdverts.Where(p => p.Currency == DAL.Entities.HelperEnum.Currency.USD);
                        break;
                    case "€":
                        flatAdverts = flatAdverts.Where(p => p.Currency == DAL.Entities.HelperEnum.Currency.Euro);
                        break;
                    case "₴":
                        flatAdverts = flatAdverts.Where(p => p.Currency == DAL.Entities.HelperEnum.Currency.UAH);
                        break;
                }
            }

            if (!String.IsNullOrEmpty(typeOfPrice))
            {
                switch (typeOfPrice)
                {
                    case "за об'єкт":
                        flatAdverts = flatAdverts.Where(p => p.TypePrice == "за об'єкт");
                        break;
                    case "за кв. метр":
                        flatAdverts = flatAdverts.Where(p => p.TypePrice == "за кв. метр");
                        break;
                }
            }

            if (maxTotalArea != null && minTotalArea != null)
            {
                if (minTotalArea == 0 && maxTotalArea == 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea != null);
                if (minTotalArea != 0 && maxTotalArea == 0)
                     flatAdverts = flatAdverts.Where(p => p.TotalArea >= minTotalArea);
                if (minTotalArea == 0 && maxTotalArea != 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea <= maxTotalArea);
                if (minTotalArea > 0 && maxTotalArea > 0)
                    flatAdverts = flatAdverts.Where(p => p.TotalArea >= minTotalArea && p.Price <= maxTotalArea);
            }

            if (maxFloor != null && minFloor != null)
            {
                if (minFloor == 0 && maxFloor == 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor != null);
                if (minFloor != 0 && maxFloor == 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor >= minFloor);
                if (minFloor == 0 && maxFloor != 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor <= maxFloor);
                if (minFloor > 0 && maxFloor > 0)
                    flatAdverts = flatAdverts.Where(p => p.Floor >= minFloor && p.Price <= maxFloor);
            }

            var count =  await flatAdverts.CountAsync();
            var flatAdverts_ = await flatAdverts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ResponseFlatAdvertDTO output = new ResponseFlatAdvertDTO()
            {
                Count = count,
                FlatAdverts = flatAdverts_,
                PageSize = pageSize
            };

            return output;
        }
    }
}
