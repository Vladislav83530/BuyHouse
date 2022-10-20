using BuyHouse.DAL.Entities.HelperEnum;

namespace BuyHouse.BLL.DTO
{
    public class HouseAdvertFilter
    {
        public string? CityName { get; set; }
        public string? CountRooms { get; set; }
        public ulong MinPrice { get; set; }
        public ulong MaxPrice { get; set; }
        public Currency Currency { get; set; }
        public TypeOfPrice TypeOfPrice { get; set; }
        public double MinTotalArea { get; set; }
        public double MaxTotalArea { get; set; }
        public double MinLandArea { get; set; }
        public double MaxLandArea { get; set; }
    }
}
