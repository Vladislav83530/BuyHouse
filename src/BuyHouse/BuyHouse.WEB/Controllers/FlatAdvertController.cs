using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Clients;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.HttpClientModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IFlatAdvertFilterService _flatAdvertService;
        private readonly IUserProfileService _userProfileService;
        private readonly BuyHouseAPIClient _client;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FlatAdvertController> _localizer;

        public FlatAdvertController(IFlatAdvertFilterService flatAdertService, 
            IUserProfileService userProfileService,
            IMapper mapper,
            IStringLocalizer<FlatAdvertController> localizer, 
            BuyHouseAPIClient  client)
        {
            _flatAdvertService = flatAdertService;
            _userProfileService = userProfileService;
            _mapper = mapper;
            _localizer = localizer;
            _client = client;
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
        public async Task<IActionResult> CreateAdvertPost( FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                try
                {
                    FlatAdvert flatAdvert_ = await _client.CreateFlatAdvertAsync(new CreateRequestModel { FlatAdvert = flatAdvertModel}, uploads, currentUserId);
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
                ResponseFlatAdvertDTO responseFlatAdvertDTO = await _flatAdvertService
                    .GetFlatAdvertByParametersAsync(filter, pageSize, page);

                var flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(responseFlatAdvertDTO.FlatAdverts);

                IndexFilterViewModel vm = new IndexFilterViewModel()
                {
                    FlatAdverts = flatAdvertShortModels,
                    FlatAdvertFilter = filter,
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
                GetFlatAdvertViewModel vm = new GetFlatAdvertViewModel
                {
                    FlatAdvert = flatAdvertModel,
                    UserProfile = userProfile
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
