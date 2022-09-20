using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Clients;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.HttpClientModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IFlatAdvertService _flatAdvertService;
        private readonly BuyHouseAPIClient _client;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FlatAdvertController> _localizer;

        public FlatAdvertController(IFlatAdvertService flatAdertService, IMapper mapper,
            IStringLocalizer<FlatAdvertController> localizer, BuyHouseAPIClient  client)
        {
            _flatAdvertService = flatAdertService;
            _mapper = mapper;
            _localizer = localizer;
            _client = client;
        }

        [HttpGet]
        public IActionResult CreateAdvert() => View();

        //TODO: currentUserId change
        [HttpPost]
        public async Task<IActionResult> CreateAdvertPost( FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = "0f8fad5b-d9cb-469f-a165-70867728950e";
                try
                {
                    FlatAdvert flatAdvert_ = await _client.CreateFlatAdvert(new CreateRequestModel { FlatAdvert = flatAdvertModel}, uploads, currentUserId);
                    TempData["AlertMessage"] = _localizer["create alert message"];
                    return RedirectToAction("GetFlatAdvert", new { flatAdvertId = flatAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = _localizer["Error advert message"] });
        }

        //TODO: Exception
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(FlatAdvertFilter filter, int pageSize, int page = 1)
        {
            ResponseFlatAdvertDTO responseFlatAdvertDTO =  await _flatAdvertService
                .GetFlatAdvertByParameters(filter, pageSize, page);

            var flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(responseFlatAdvertDTO.FlatAdverts);

            IndexViewModel vm = new IndexViewModel()
            {
                FlatAdverts = flatAdvertShortModels,            
                FlatAdvertFilter = filter,
                PageViewModel = new PageViewModel(responseFlatAdvertDTO.Count, page, responseFlatAdvertDTO.PageSize)
            };
            return View(vm);
        }

        //TODO: change view | add info about user 
        [HttpGet]
        public async Task<IActionResult> GetFlatAdvert(int? flatAdvertId)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                FlatAdvertModel flatAdvertModel = new FlatAdvertModel();
                var flatAdvert = await _client.GetFlatAdvertByID(flatAdvertId);

                flatAdvertModel =  _mapper.Map<FlatAdvert, FlatAdvertModel>(flatAdvert);
                GetFlatAdvertViewModel vm = new GetFlatAdvertViewModel
                {
                    FlatAdvert = flatAdvertModel
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
