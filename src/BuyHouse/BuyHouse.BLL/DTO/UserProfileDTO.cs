using BuyHouse.DAL.Entities.ApplicationUserEntities;


namespace BuyHouse.BLL.DTO
{
    public class UserProfileDTO
    {
        public string? UserProfileId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public UserAvatar? UserAvatar { get; set; }
    }
}
