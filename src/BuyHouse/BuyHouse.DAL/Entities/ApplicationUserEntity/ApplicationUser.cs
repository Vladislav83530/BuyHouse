using Microsoft.AspNetCore.Identity;

namespace BuyHouse.DAL.Entities.ApplicationUserEntity
{
    public class ApplicationUser : IdentityUser
    {
        public string UserSurname { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public ICollection<Advert> Adverts { get; set; }
    }
}
