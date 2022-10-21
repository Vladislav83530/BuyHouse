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
    public class FlatAdvertController : Controller
    {
        private readonly IAdvertFilterService<FlatAdvert, FlatAdvertFilter> _flatAdvertService;
        private readonly IUserProfileService _userProfileService;
        private readonly BuyHouseAPIClient _client;
        private readonly IPhotosService _photosService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FlatAdvertController> _localizer;

        public FlatAdvertController(IAdvertFilterService<FlatAdvert, FlatAdvertFilter> flatAdertService, 
            IUserProfileService userProfileService,
            IMapper mapper,
            IStringLocalizer<FlatAdvertController> localizer, 
            BuyHouseAPIClient client,
            IPhotosService photosService)
        {
            _flatAdvertService = flatAdertService;
            _userProfileService = userProfileService;
            _mapper = mapper;
            _localizer = localizer;
            _client = client;
            _photosService = photosService;
        }

        /// <summary>
        /// CreateAdvert view
        /// </summary>
        /// <returns>View for creating advert</returns>
        [HttpGet]
        [Authorize]
        public IActionResult CreateAdvert() => View();

        /// <summary>
        /// Create flat advert
        /// </summary>
        /// <param name="flatAdvertModel"></param>
        /// <param name="uploads"></param>
        /// <returns>Created advert</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAdvertPost(FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                try
                {
                    FlatAdvert flatAdvert_ = await _client.CreateFlatAdvertAsync(flatAdvertModel, uploads, currentUserId);
                    return RedirectToAction("GetFlatAdvert", new { flatAdvertId = flatAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = _localizer["Error advert message"] });
        }

        /// <summary>
        /// Search flat adverts
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>View with filtered adverts</returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(FlatAdvertFilter filter, int pageSize, int page = 1)
        {
            try
            {
                ResponseAdvertDTO<FlatAdvert> responseFlatAdvertDTO = await _flatAdvertService
                    .GetAdvertByParametersAsync(filter, pageSize, page);

                var flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(responseFlatAdvertDTO.Adverts);

                IndexFilterViewModel<FlatAdvertShortModel, FlatAdvertFilter> vm = new IndexFilterViewModel<FlatAdvertShortModel, FlatAdvertFilter>()
                {
                    RealtyAdverts = flatAdvertShortModels,
                    RealtyAdvertFilter = filter,
                    PageViewModel = new PageViewModel(responseFlatAdvertDTO.Count, page, responseFlatAdvertDTO.PageSize)
                };
                return View(vm);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// get flat advert with info about user
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <returns>View with advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/{flatAdvertId:int}")]
        public async Task<IActionResult> GetFlatAdvert(int? flatAdvertId)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                FlatAdvertModel flatAdvertModel = new FlatAdvertModel();
                var flatAdvert = await _client.GetFlatAdvertByIDAsync(flatAdvertId);

                var userProfile = await _userProfileService.GetUserProfileInfoAsync(flatAdvert.UserID);

                flatAdvertModel =  _mapper.Map<FlatAdvert, FlatAdvertModel>(flatAdvert);
                GetAdvertViewModel<FlatAdvertModel> vm = new GetAdvertViewModel<FlatAdvertModel>
                {
                    Advert = flatAdvertModel,
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
        /// Edit advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <returns>get view for editing advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/[action]/{flatAdvertId:int}")]
        [Authorize]
        public async Task<IActionResult> EditAdvert(int flatAdvertId)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                FlatAdvertModel flatAdvertModel = new FlatAdvertModel();
                var flatAdvert = await _client.GetFlatAdvertByIDAsync(flatAdvertId);
                flatAdvertModel = _mapper.Map<FlatAdvert, FlatAdvertModel>(flatAdvert);
                return View(flatAdvertModel);    
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// edit advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <param name="flatAdvertModel"></param>
        /// <param name="uploads"></param>
        /// <returns>edited flat advert or error page</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditFlatAdvert(int flatAdvertId, FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                try
                {
                    FlatAdvert flatAdvert_ = await _client.UpdateFlatAdvertAsync(flatAdvertId, flatAdvertModel, uploads, currentUserId);
                    return RedirectToAction("GetFlatAdvert", new { flatAdvertId = flatAdvert_.Id });
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
        public async Task<JsonResult> DeleteFlatAdvertPhoto(int photoId, int flatAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var advert = await _client.GetFlatAdvertByIDAsync(flatAdvertId);
            var flatAdvertModel = _mapper.Map<FlatAdvert, FlatAdvertModel>(advert);
            if (flatAdvertModel.Photos.Count != 1)
            {
                FlatAdvert flatAdvert = await _photosService.DeletePhotoFromFlatAdvertAsync(currentUserId, flatAdvertId, photoId);
                return Json(flatAdvert);
            }
            else
               return Json(advert);
        }

        /// <summary>
        /// Delete flat advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <returns>View with sellers adverts or error page</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteFlatAdvert(int flatAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _client.DeleteFlatAdvertAsync(flatAdvertId, currentUserId);
                if (result != null)
                    return RedirectToAction("GetSellersAdverts", "UserProfile");
                return BadRequest();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}