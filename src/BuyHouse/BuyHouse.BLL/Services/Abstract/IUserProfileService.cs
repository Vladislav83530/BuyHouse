using BuyHouse.BLL.DTO;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IUserProfileService
    {
        public Task<UserProfileDTO> GetUserProfileInfoAsync(string? currentUserId);
        public Task UpdateUserInfoAsync(UserProfileDTO userInfo, string currentUserId);
    }
}
