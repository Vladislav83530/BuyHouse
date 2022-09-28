using BuyHouse.BLL.DTO;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IUserProfileService
    {
        public Task<UserProfileDTO> GetUserProfileInfo(string? currentUserId);
        public Task UpdateUserInfo(UserProfileDTO userInfo, string currentUserId);
    }
}
