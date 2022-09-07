using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.AspNetCore.Identity;

namespace BuyHouse.DAL.Entities.ApplicationUserEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string? UserSurname { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public ICollection<FlatAdvert>? FlatAdverts { get; set; }
        public ICollection<RoomAdvert>? RoomAdverts { get; set; }
        public ICollection<HouseAdvert>? HouseAdverts { get; set; }
    }
}
