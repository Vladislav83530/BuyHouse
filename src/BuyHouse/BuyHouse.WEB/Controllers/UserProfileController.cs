using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services;
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
        private readonly IAdvertFilterService<FlatAdvert, FlatAdvertFilter> _flatAdvertService;
        private readonly IMapper _mapper;
        private readonly IAdvertFilterService<HouseAdvert, HouseAdvertFilter> _houseAdvertFilterService;
        private readonly IAdvertFilterService<RoomAdvert, RoomAdvertFilter> _roomAdvertFilterService;

        public UserProfileController(IUserProfileService userProfile, 
            IPhotosService photoService, 
            ApplicationDbContext context,
            IStringLocalizer<UserProfileController> localizer,
            IAdvertFilterService<FlatAdvert, FlatAdvertFilter> flatAdvertService, 
            IMapper mapper,
            IAdvertFilterService<HouseAdvert, HouseAdvertFilter> houseAdvertFilterService,
            IAdvertFilterService<RoomAdvert, RoomAdvertFilter> roomAdvertFilterService)
        {
            _userProfile = userProfile;
            _photoService = photoService;
            _context = context;
            _localizer = localizer;
            _flatAdvertService = flatAdvertService;
            _mapper = mapper;
            _houseAdvertFilterService = houseAdvertFilterService;
            _roomAdvertFilterService = roomAdvertFilterService;
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
                var currentUser = await _userProfile.GetUserProfileInfoAsync(currentUserId);
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
                 await _photoService.UpdateUserAvatarPhotoAsync(uploadedFile, currentUsersAvatar, currentUserId);
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
                await _userProfile.UpdateUserInfoAsync(userProfileInfo, currentUserId);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// Get Sellers adverts
        /// </summary>
        /// <returns>View with sellers adverts</returns>
        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("[controller]/SellersAdverts")]
        public async Task<IActionResult> GetSellersAdverts()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);

            try
            {
                IEnumerable<FlatAdvert> flatAdverts = await _flatAdvertService.GetSellersAdvertsAsync(currentUserId);
                IEnumerable<FlatAdvertShortModel> flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(flatAdverts);

                IEnumerable<HouseAdvert> houseAdverts = await _houseAdvertFilterService.GetSellersAdvertsAsync(currentUserId);
                IEnumerable<HouseAdvertShortModel> houseAdvertShortModels = _mapper.Map<IEnumerable<HouseAdvert>, List<HouseAdvertShortModel>>(houseAdverts);

                IEnumerable<RoomAdvert> roomAdverts = await _roomAdvertFilterService.GetSellersAdvertsAsync(currentUserId);
                List<RoomAdvertShortModel> roomAdvertShortModels = _mapper.Map<IEnumerable<RoomAdvert>, List<RoomAdvertShortModel>>(roomAdverts);

                return View(new Sellers_LikeAdvertsViewModel { FlatAdverts = flatAdvertShortModels, HouseAdverts =houseAdvertShortModels, RoomAdverts = roomAdvertShortModels });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }


        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("[controller]/LikedAdverts")]
        public async Task<IActionResult> GetLikedAdverts()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(currentUserId))
                return NotFound(_localizer["Not found user id error"]);

            try
            {
                IEnumerable<FlatAdvert> flatAdverts = await _flatAdvertService.GetLikedAdvertsByUserAsync(currentUserId);
                IEnumerable<FlatAdvertShortModel> flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(flatAdverts);

                IEnumerable<HouseAdvert> houseAdverts = await _houseAdvertFilterService.GetLikedAdvertsByUserAsync(currentUserId);
                IEnumerable<HouseAdvertShortModel> houseAdvertShortModels = _mapper.Map<IEnumerable<HouseAdvert>, List<HouseAdvertShortModel>>(houseAdverts);

                IEnumerable<RoomAdvert> roomAdverts = await _roomAdvertFilterService.GetLikedAdvertsByUserAsync(currentUserId);
                List<RoomAdvertShortModel> roomAdvertShortModels = _mapper.Map<IEnumerable<RoomAdvert>, List<RoomAdvertShortModel>>(roomAdverts);

                return View(new Sellers_LikeAdvertsViewModel { FlatAdverts = flatAdvertShortModels, HouseAdverts = houseAdvertShortModels, RoomAdverts = roomAdvertShortModels });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
