using AutoMapper;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.AdvertModel;
using Microsoft.AspNetCore.Mvc;

namespace BuyHouse.WEB.Controllers
{
    public class HouseAdvertController : Controller
    {
        private readonly IAdvertService<HouseAdvertDTO, HouseAdvert> _houseAdvertService;
        private readonly IMapper _mapper;

        public HouseAdvertController(IAdvertService<HouseAdvertDTO, HouseAdvert> houseAdvertService)
        {
            _houseAdvertService = houseAdvertService;
            _mapper = new Mapper(AutoMapper_WEB.GetMapperConfiguration());
        }

        [HttpGet]
        public IActionResult CreateAdvert() => View();

        //TODO: currentUserId change | Redirect to action
        [HttpPost]
        public async Task<IActionResult> CreateAdvertPost(HouseAdvertModel houseAdvertModel, IFormFileCollection uploads)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = "0f8fad5b-d9cb-469f-a165-70867728950e";
                try
                {
                    HouseAdvertDTO houseAdvertDTO = _mapper.Map<HouseAdvertModel, HouseAdvertDTO>(houseAdvertModel);
                    HouseAdvertDTO houseAdvertDTO_ = await _houseAdvertService.Create(houseAdvertDTO, uploads, currentUserId);

                    TempData["AlertMessage"] = "Your advert was created successfully! If you want to change the information in the advert, go to your profile!";
                    return RedirectToAction("GetHouseAdvert", new { houseAdvertId = houseAdvertDTO_.Id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = "Invalid flat advertising" });
        }

        //TODO: add HouseAdvertModel and ViewModel      
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var houseAdvertDTOs = await _houseAdvertService.GetAll();
            List<HouseAdvertModel> houseAdvertModels = new List<HouseAdvertModel>();
            houseAdvertModels = _mapper.Map<IEnumerable<HouseAdvertDTO>, List<HouseAdvertModel>>(houseAdvertDTOs);
            return View(houseAdvertModels);
        }

        //TODO: change RedirectToAction
        [HttpGet]
        public async Task<IActionResult> DeleteHouseAdvert(int? houseAdvertId)
        {
            if (houseAdvertId == null)
                return RedirectToAction("Error", "Home");
            await _houseAdvertService.Delete(houseAdvertId);
            return RedirectToAction("Index", "Home");
        }

        //TODO: change view | add info about user 
        [HttpGet]
        public async Task<IActionResult> GetHouseAdvert(int? houseAdvertId)
        {
            if (houseAdvertId == null)
                return RedirectToAction("Error", "Home");

            try
            {
                HouseAdvertModel houseAdvertModel = new HouseAdvertModel();
                var houseAdvertDTO = await _houseAdvertService.GetById(houseAdvertId);
                houseAdvertModel = _mapper.Map<HouseAdvertDTO, HouseAdvertModel>(houseAdvertDTO);
                return View(houseAdvertModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
