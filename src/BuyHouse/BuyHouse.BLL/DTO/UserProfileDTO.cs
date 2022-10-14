using BuyHouse.DAL.Entities.ApplicationUserEntities;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.BLL.DTO
{
    public class UserProfileDTO
    {
        public string? UserProfileId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        [RegularExpression(@"^(?:\+38)?(0[5-9][0-9]\d{7})$", ErrorMessage = "Incorrect phone number. Enter format (+380XXXXXXXXX)")]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public UserAvatar? UserAvatar { get; set; }
    }
}
