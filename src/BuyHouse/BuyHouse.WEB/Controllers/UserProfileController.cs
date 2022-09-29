using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BuyHouse.WEB.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfile;
        private readonly IPhotosService _photoService;
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<UserProfileController> _localizer;

        public UserProfileController(IUserProfileService userProfile, IPhotosService photoService, ApplicationDbContext context,
            IStringLocalizer<UserProfileController> localizer)
        {
            _userProfile = userProfile;
            _photoService = photoService;
            _context = context;
            _localizer = localizer;
        }

        /// <summary>
        /// User`s office page
        /// </summary>
        /// <returns>view with user`s info</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);

            try
            {
                var currentUser = await _userProfile.GetUserProfileInfo(currentUserId);
                return View(currentUser);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// update user avatar
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns>view with updated avatar</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUsersAvatar(IFormFile uploadedFile)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUsersAvatar = await  _context.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserId);

            if (currentUsersAvatar == null)
                return NotFound(_localizer["Not found user photo"]);

            if (String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);

            try
            {
                 await _photoService.UpdateUserAvatarPhoto(uploadedFile, currentUsersAvatar, currentUserId);
                 return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// Update user main info
        /// </summary>
        /// <param name="userProfileInfo"></param>
        /// <returns>View with edited information</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(UserProfileDTO userProfileInfo)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);
            try
            {
                await _userProfile.UpdateUserInfo(userProfileInfo, currentUserId);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
