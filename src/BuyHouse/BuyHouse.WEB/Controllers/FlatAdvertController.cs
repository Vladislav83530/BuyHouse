using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IAdvertService<FlatAdvert> _advertService;
        private readonly IFlatAdvertService _flatAdvertService;
        private readonly IMapper _mapper;

        public FlatAdvertController(IAdvertService<FlatAdvert> advertService, IFlatAdvertService flatAdertService)
        {
            _advertService = advertService;
            _flatAdvertService = flatAdertService;
            _mapper = new Mapper(AutoMapper_WEB.GetMapperConfiguration());
        }

        [HttpGet]
        public IActionResult CreateAdvert() => View();

        //TODO: currentUserId change | Redirect to action
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

                    TempData["AlertMessage"] = "Your advert was created successfully! If you want to change the information in the advert, go to your profile!";
                    return RedirectToAction("GetFlatAdvert", new { flatAdvertId = flatAdvert_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = "Invalid flat advertising" });
        }

        //TODO: Exception
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(
            string nameCity, 
            string countRooms,
            int minPrice, int maxPrice, 
            string currency, 
            string typeOfPrice,
            double minTotalArea, double maxTotalArea, 
            int minFloor, int maxFloor,
            int page = 1)
        {
            ResponseFlatAdvertDTO responseFlatAdvertDTO =  await _flatAdvertService
                .GetFlatAdvertByParameters(
                nameCity, 
                countRooms,
                minPrice, maxPrice, currency, typeOfPrice,
                minTotalArea, maxTotalArea,
                minFloor, maxFloor,
                page);

            var flatAdvertShortModels = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(responseFlatAdvertDTO.FlatAdverts);

            IndexViewModel vm = new IndexViewModel()
            {
                FlatAdverts = flatAdvertShortModels,            
                FilterViewModel = new FilterViewModel 
                {
                    SelectedCityName = nameCity, 
                    CountRooms = new SelectList(new List<string> {"Усі","1","2","3","4+"}),
                    SelectedMaxPrice = maxPrice, 
                    SelectedMinPrice =minPrice,
                    Currency = new SelectList(new List<string> { "Будь-яка", "$", "€", "₴" }),
                    TypeOfPrice = new SelectList(new List<string> {"за об'єкт", "за кв. метр"}),
                    SelectedMinTotalArea = minTotalArea,
                    SelectedMaxTotalArea = maxTotalArea,
                    SelecetedMinFloor = minFloor,
                    SelectedMaxFloor = maxFloor
                },
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
