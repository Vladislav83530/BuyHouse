using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;

namespace BuyHouse.DAL.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int AdvertId { get; set; }
        public TypeOfRealtyAdvert TypeOfRealty { get; set; }
    }

    public enum TypeOfRealtyAdvert
    {
        FlatAdvert,
        HouseAdvert,
        RoomAdvert
    }
}
