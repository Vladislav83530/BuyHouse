using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.AspNetCore.Identity;

namespace BuyHouse.DAL.Entities.ApplicationUserEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public ICollection<FlatAdvert>? FlatAdverts { get; set; }
        public ICollection<RoomAdvert>? RoomAdverts { get; set; }
        public ICollection<HouseAdvert>? HouseAdverts { get; set; }
        public ICollection<Like>? Likes { get; set; } 
        public UserAvatar? UserAvatar { get; set; }
    }
}
