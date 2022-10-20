using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.BLL.DTO
{
    public class ResponseHouseAdvertDTO
    {
        public IEnumerable<HouseAdvert>? HouseAdverts {get ;set;}
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}
