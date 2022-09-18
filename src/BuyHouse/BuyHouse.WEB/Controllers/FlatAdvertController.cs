using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IAdvertService<FlatAdvert> _advertService;
        private readonly IFlatAdvertService _flatAdvertService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<FlatAdvertController> _localizer;

        public FlatAdvertController(IAdvertService<FlatAdvert> advertService, IFlatAdvertService flatAdertService, IMapper mapper, IStringLocalizer<FlatAdvertController> localizer)
        {
            _advertService = advertService;
            _flatAdvertService = flatAdertService;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult CreateAdvert() => View();

        //TODO: currentUserId change
        [HttpPost]
        public async Task<IActionResult> CreateAdvertPost(FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = "0f8fad5b-d9cb-469f-a165-70867728950e";
                try
                {
                    FlatAdvert flatAdvert = _mapper.Map<FlatAdvertModel, FlatAdvert>(flatAdvertModel);
                    FlatAdvert flatAdvert_ = await _advertService.CreateAdvertAsync(flatAdvert, uploads, currentUserId);

                    TempData["AlertMessage"] = "Your advert was created successfully!If you want to change the information in the advert, go to your profile!";
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
                var flatAdvert = await _advertService.FindAdvertByIdAsync(flatAdvertId);
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
