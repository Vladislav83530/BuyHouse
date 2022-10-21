using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services;
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
    public class RoomAdvertController : Controller
    {
        private readonly BuyHouseAPIClient _client;
        private readonly IStringLocalizer<RoomAdvertController> _localizer;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly IAdvertFilterService<RoomAdvert, RoomAdvertFilter> _roomAdvertFilterService;
        private readonly IPhotosService _photosService;

        public RoomAdvertController(BuyHouseAPIClient client,
            IStringLocalizer<RoomAdvertController> localizer,
            IMapper mapper,
            IUserProfileService userProfileService,
            IAdvertFilterService<RoomAdvert, RoomAdvertFilter> roomAdvertFilterService,
            IPhotosService photosService)
        {
            _client = client;
            _localizer = localizer;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _roomAdvertFilterService = roomAdvertFilterService;
            _photosService = photosService;
        }

        /// <summary>
        /// Get view for creating advert
        /// </summary>
        /// <returns>View for creating</returns>
        [HttpGet]
        [Authorize]
        public IActionResult CreateAdvert() => View();

        /// <summary>
        /// Create room advert
        /// </summary>
        /// <param name="roomAdvert"></param>
        /// <param name="uploads"></param>
        /// <returns>Created room advert</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAdvertPost(RoomAdvertModel roomAdvert, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                try
                {
                    RoomAdvert roomAdvert_ = await _client.CreateRoomAdvertAsync(roomAdvert, uploads, currentUserId);
                    return RedirectToAction("GetRoomAdvert", new { roomAdvertId = roomAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = _localizer["Error advert message"] });
        }

        /// <summary>
        /// Get info about room 
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <returns>Room advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/{roomAdvertId:int}")]
        public async Task<IActionResult> GetRoomAdvert(int roomAdvertId)
        {
            if (roomAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                RoomAdvertModel roomAdvertModel = new RoomAdvertModel();
                var roomAdvert = await _client.GetRoomAdvertByIdAsync(roomAdvertId);

                var userProfile = await _userProfileService.GetUserProfileInfoAsync(roomAdvert.UserID);

                roomAdvertModel = _mapper.Map<RoomAdvert, RoomAdvertModel>(roomAdvert);
                GetAdvertViewModel<RoomAdvertModel> vm = new GetAdvertViewModel<RoomAdvertModel>
                {
                    Advert = roomAdvertModel,
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
        /// Search room adverts
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>View with filtered adverts</returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(RoomAdvertFilter filter, int pageSize, int page = 1)
        {
            try
            {
                ResponseAdvertDTO<RoomAdvert> responseRoomAdvertDTO = await _roomAdvertFilterService
                    .GetAdvertByParametersAsync(filter, pageSize, page);

                var roomAdvertShortModels = _mapper.Map<IEnumerable<RoomAdvert>, List<RoomAdvertShortModel>>(responseRoomAdvertDTO.Adverts);

                IndexFilterViewModel<RoomAdvertShortModel, RoomAdvertFilter> vm = new IndexFilterViewModel<RoomAdvertShortModel, RoomAdvertFilter>()
                {
                    RealtyAdverts = roomAdvertShortModels,
                    RealtyAdvertFilter = filter,
                    PageViewModel = new PageViewModel(responseRoomAdvertDTO.Count, page, responseRoomAdvertDTO.PageSize)
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
        /// <param name="roomAdvertId"></param>
        /// <returns>get view for editing advert</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/[controller]/[action]/{roomAdvertId:int}")]
        [Authorize]
        public async Task<IActionResult> EditAdvert(int roomAdvertId)
        {
            if (roomAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                RoomAdvertModel roomAdvertModel = new RoomAdvertModel();
                var roomAdvert = await _client.GetRoomAdvertByIdAsync(roomAdvertId);
                roomAdvertModel = _mapper.Map<RoomAdvert, RoomAdvertModel>(roomAdvert);
                return View(roomAdvertModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        /// <summary>
        /// edit advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <param name="roomAdvertModel"></param>
        /// <param name="uploads"></param>
        /// <returns>edited room advert or error page</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditRoomAdvert(int roomAdvertId, RoomAdvertModel roomAdvertModel, IFormFileCollection uploads)
        {
            if (roomAdvertId == null)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                try
                {
                    RoomAdvert roomAdvert_ = await _client.UpdateRoomAdvertAsync(roomAdvertId, roomAdvertModel, uploads, currentUserId);
                    return RedirectToAction("GetRoomAdvert", new { roomAdvertId = roomAdvert_.Id });
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
        /// <param name="roomAdvertId"></param>
        /// <returns>json with advert and list of photo</returns>
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> DeleteRoomAdvertPhoto(int photoId, int roomAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var advert = await _client.GetRoomAdvertByIdAsync(roomAdvertId);
            var roomAdvertModel = _mapper.Map<RoomAdvert, RoomAdvertModel>(advert);
            if (roomAdvertModel.Photos.Count != 1)
            {
                RoomAdvert roomAdvert = await _photosService.DeletePhotoFromRoomAdvertAsync(currentUserId, roomAdvertId, photoId);
                return Json(roomAdvert);
            }
            else
                return Json(advert);
        }

        /// <summary>
        /// Delete room advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <returns>View with sellers adverts or error page</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteRoomAdvert(int roomAdvertId)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _client.DeleteRoomAdvertAsync(roomAdvertId, currentUserId);
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
