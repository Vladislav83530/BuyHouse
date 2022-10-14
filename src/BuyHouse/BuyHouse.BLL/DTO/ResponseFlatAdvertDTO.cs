using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.BLL.DTO
{
    public class ResponseFlatAdvertDTO
    {
        public IEnumerable<FlatAdvert>? FlatAdverts {get ;set;}
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}
