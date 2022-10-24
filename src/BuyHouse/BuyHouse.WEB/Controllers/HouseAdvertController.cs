using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Clients;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BuyHouse.WEB.Controllers
{
    public class HouseAdvertController : Controller
    {
        private readonly BuyHouseAPIClient _client;
        private readonly IStringLocalizer<HouseAdvertController> _localizer;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly IAdvertFilterService<HouseAdvert, HouseAdvertFilter> _houseAdvertFilterService;
        private readonly IPhotosService _photosService;
        private readonly ILikeAdvertService _likeAdvertService;

        public HouseAdvertController(BuyHouseAPIClient client, 
            IStringLocalizer<HouseAdvertController> localizer,
            IMapper mapper,
            IUserProfileService userProfileService,
            IAdvertFilterService<HouseAdvert, HouseAdvertFilter> houseAdvertFilterService,
            IPhotosService photosService,
            ILikeAdvertService likeAdvertService)
        {
            _client = client;
            _localizer = localizer;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _houseAdvertFilterService = houseAdvertFilterService;
            _photosService = photosService;
            _likeAdvertService = likeAdvertService;
        }

        /// <summary>
        /// Get view for creating advert
        /// </summary>
        /// <returns>View for creating</returns>
        [HttpGet]
        [Authorize]
        public IActionResult CreateAdvert() => View();

        /// <summary>
        /// Create house advert
        /// </summary>
        /// <param name="houseAdvert"></param>
        /// <param name="uploads"></param>
        /// <returns>Created house advert</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAdvertPost(HouseAdvertModel houseAdvert, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                try
                {
                    HouseAdvert houseAdvert_ = await _client.CreateHouseAdvertAsync(houseAdvert, uploads, currentUserId);
                    return RedirectToAction("GetHouseAdvert", new { houseAdvertId = houseAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = _localizer["Error advert message"] });
        }

        /// <summary>
        /// Get info about house
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <returns>House advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/{houseAdvertId:int}")]
        public async Task<IActionResult> GetHouseAdvert(int houseAdvertId)
        {
            if (houseAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                HouseAdvertModel houseAdvertModel = new HouseAdvertModel();
                var houseAdvert = await _client.GetHouseAdvertByIdAsync(houseAdvertId);

                var userProfile = await _userProfileService.GetUserProfileInfoAsync(houseAdvert.UserID);

                houseAdvertModel = _mapper.Map<HouseAdvert, HouseAdvertModel>(houseAdvert);
                GetAdvertViewModel<HouseAdvertModel> vm = new GetAdvertViewModel<HouseAdvertModel>
                {
                    Advert = houseAdvertModel,
                    UserProfile = userProfile
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// Serch page for house advert
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>House adverts by parameters</returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(HouseAdvertFilter filter, int pageSize, int page = 1)
        {
            try
            {
                ResponseAdvertDTO<HouseAdvert> responseHouseAdvertDTO = await _houseAdvertFilterService
                    .GetAdvertByParametersAsync(filter, pageSize, page);

                var houseAdvertShortModels = _mapper.Map<IEnumerable<HouseAdvert>, List<HouseAdvertShortModel>>(responseHouseAdvertDTO.Adverts);

                IndexFilterViewModel<HouseAdvertShortModel, HouseAdvertFilter> vm = new IndexFilterViewModel<HouseAdvertShortModel, HouseAdvertFilter>()
                {
                    RealtyAdverts = houseAdvertShortModels,
                    RealtyAdvertFilter = filter,
                    PageViewModel = new PageViewModel(responseHouseAdvertDTO.Count, page, responseHouseAdvertDTO.PageSize)
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// Edit advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <returns>get view for editing advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/[action]/{houseAdvertId:int}")]
        [Authorize]
        public async Task<IActionResult> EditAdvert(int houseAdvertId)
        {
            if (houseAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                HouseAdvertModel houseAdvertModel = new HouseAdvertModel();
                var houseAdvert = await _client.GetHouseAdvertByIdAsync(houseAdvertId);
                houseAdvertModel = _mapper.Map<HouseAdvert, HouseAdvertModel>(houseAdvert);
                return View(houseAdvertModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// edit advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="houseAdvertModel"></param>
        /// <param name="uploads"></param>
        /// <returns>edited house advert or error page</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditHouseAdvert(int houseAdvertId, HouseAdvertModel houseAdvertModel, IFormFileCollection uploads)
        {
            if (houseAdvertId == null)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                try
                {
                    HouseAdvert houseAdvert_ = await _client.UpdateHouseAdvertAsync(houseAdvertId, houseAdvertModel, uploads, currentUserId);
                    return RedirectToAction("GetHouseAdvert", new { houseAdvertId = houseAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = _localizer["Error advert message"] });
        }

        /// <summary>
        /// Delete photos from advert
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="flatAdvertId"></param>
        /// <returns>json with advert and list of photo</returns>
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> DeleteHouseAdvertPhoto(int photoId, int houseAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var advert = await _client.GetHouseAdvertByIdAsync(houseAdvertId);
            var houseAdvertModel = _mapper.Map<HouseAdvert, HouseAdvertModel>(advert);
            if (houseAdvertModel.Photos.Count != 1)
            {
                HouseAdvert flatAdvert = await _photosService.DeletePhotoFromHouseAdvertAsync(currentUserId, houseAdvertId, photoId);
                return Json(flatAdvert);
            }
            else
                return Json(advert);
        }

        /// <summary>
        /// Delete house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <returns>View with sellers adverts or error page</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteHouseAdvert(int houseAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _client.DeleteHouseAdvertAsync(houseAdvertId, currentUserId);
                if (result != null)
                    return RedirectToAction("GetSellersAdverts", "UserProfile");
                return BadRequest();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// Like house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <returns>Count of likes</returns>
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> LikeHouseAdvert(int houseAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _likeAdvertService.LikeHouseAdvert(houseAdvertId, currentUserId);
            return Json(result);
        }
    }
}
