using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
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
        private readonly IFlatAdvertService _flatAdvertService;
        private readonly IMapper _mapper;

        public UserProfileController(IUserProfileService userProfile, 
            IPhotosService photoService, 
            ApplicationDbContext context,
            IStringLocalizer<UserProfileController> localizer,
            IFlatAdvertService flatAdvertService, 
            IMapper mapper)
        {
            _userProfile = userProfile;
            _photoService = photoService;
            _context = context;
            _localizer = localizer;
            _flatAdvertService = flatAdvertService;
            _mapper = mapper;
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

        [HttpGet]
        [Authorize]
        [Route("[controller]/SellersAdverts")]
        public async Task<IActionResult> GetSellersAdverts()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);

            try
            {
                IEnumerable<FlatAdvert> flatAdverts = await _flatAdvertService.GetSellersFlatAdverts(currentUserId);
                IEnumerable<FlatAdvertShortModel> flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(flatAdverts);
                return View(new SellersAdvertsViewModel { FlatAdverts = flatAdvertShortModels });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
