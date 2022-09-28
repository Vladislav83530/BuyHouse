using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _context;

        public UserProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get information about user by user Id
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>Main information about user</returns>
        public async Task<UserProfileDTO> GetUserProfileInfo(string? currentUserId)
        {
            var currentUser = await GetCurrentUserById(currentUserId);
            var currentUserAvatar = await _context.UserAvatars.FirstOrDefaultAsync(avatar=>avatar.ApplicationUserId == currentUserId);

            UserProfileDTO userProfile = new UserProfileDTO
            {
                UserProfileId= currentUserId,
                Name = currentUser.Name,
                Surname = currentUser.Surname,
                Region = currentUser.Region,
                City =currentUser.City,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                UserAvatar = currentUserAvatar
            };

            return userProfile;
        }

        /// <summary>
        /// Update user`s information
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task UpdateUserInfo(UserProfileDTO userInfo, string currentUserId)
        {
            var currentUser = await GetCurrentUserById(currentUserId);

            currentUser.Name = userInfo.Name;
            currentUser.Surname = userInfo.Surname;
            currentUser.PhoneNumber = userInfo.PhoneNumber;
            currentUser.Region = userInfo.Region;
            currentUser.City = userInfo.City;

            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get current user (Type: ApplicationUser)
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>current user full info</returns>
        /// <exception cref="Exception"></exception>
        private async Task<ApplicationUser> GetCurrentUserById(string? currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
                throw new Exception("User Id can not be empty or null");

            var currentUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == currentUserId);

            if (currentUser == null)
                throw new Exception("Not found user");

            return currentUser;
        }
    }
}
