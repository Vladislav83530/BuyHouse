using AutoMapper;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.AdvertModel;
using Microsoft.AspNetCore.Mvc;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IAdvertService<FlatAdvertDTO, FlatAdvert> _flatAdvertService;
        private readonly IMapper _mapper;

        public FlatAdvertController(IAdvertService<FlatAdvertDTO, FlatAdvert> flatAdvertService)
        {
            _flatAdvertService = flatAdvertService;
            _mapper = new Mapper(AutoMapper_WEB.GetMapperConfiguration());
        }

        [HttpGet]
        public IActionResult CreateAdvert() => View();

        //TODO: currentUserId change | Redirect to action
        [HttpPost]
        public async Task<IActionResult> CreateAdvertPost(FlatAdvertModel flatAdvertModel, IFormFileCollection uploads)
        {
            #region Test Data
            //RealtyPhotoDTO photoDTO1 = new RealtyPhotoDTO() { Name = "room1", Path = "a/b/c/" };
            //RealtyPhotoDTO photoDTO2 = new RealtyPhotoDTO() { Name = "room2", Path = "a/b/d/" };
            //List<RealtyPhotoDTO> photos = new List<RealtyPhotoDTO>();
            //photos.Add(photoDTO1);
            //photos.Add(photoDTO2);
            //FlatAdvertDTO flatAdvert = new FlatAdvertDTO()
            //{
            //    MainInfo = new RealtyMainInfoDTO()
            //    {
            //        City = "Lviv",
            //        Region = "Lviv region",
            //        Street = "Some street",
            //        FlatNumber = 23,
            //        HouseNumber = "21/1",
            //        RegistrationDate = DateTime.Today
            //    },
            //    Description = "Some main description",
            //    Type = "Secondary",
            //    Rooms = 4,
            //    TypeOfWalls = DAL.Entities.HelperEnum.TypeOfWalls.Brick,
            //    TotalArea = 200,
            //    LivingArea = 180,
            //    Floor = 7,
            //    Heating = "централізоване",
            //    YearBuilt = 2021,
            //    RegistrationNumber = "123476489312",
            //    Price = 32121312,
            //    Currency = DAL.Entities.HelperEnum.Currency.UAH,
            //    TypePrice = "за об'єкт",
            //    LikeCount = 56
            //};
            #endregion

            if (ModelState.IsValid)
            {
                string? currentUserId = "0f8fad5b-d9cb-469f-a165-70867728950e";
                try
                {   FlatAdvertDTO flatAdvertDTO = _mapper.Map<FlatAdvertModel, FlatAdvertDTO>(flatAdvertModel);
                    await _flatAdvertService.Create(flatAdvertDTO, uploads, currentUserId);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { exception = ex.Message });
                }
            }
            return RedirectToAction("Error", "Home", new { exception = "Invalid flat advertising" });
        }

        //TODO: add FlatAdvertModel and ViewModel      
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var flatAdvertDTOs = await _flatAdvertService.GetAll ();
            List<FlatAdvertModel> flatAdvertModels = new List<FlatAdvertModel>();
            flatAdvertModels = _mapper.Map<IEnumerable<FlatAdvertDTO>, List<FlatAdvertModel>>(flatAdvertDTOs);
            return View(flatAdvertModels);
        }

        //TODO: change RedirectToAction
        [HttpGet]
        public async Task<IActionResult> DeleteFlatAdvert(int? flatAdvertId)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");
            await _flatAdvertService.Delete(flatAdvertId);
            return RedirectToAction("Index", "Home");
        }

        //TODO: change view 
        [HttpGet]
        public async Task<IActionResult> GetFlatAdvert(int? flatAdvertId)
        {
            if (flatAdvertId == null)
                return RedirectToAction("Error", "Home");

            try 
            {
                FlatAdvertModel flatAdvertModel = new FlatAdvertModel();
                var flatAdvertDTO = await _flatAdvertService.GetById(flatAdvertId);
                flatAdvertModel = _mapper.Map<FlatAdvertDTO, FlatAdvertModel>(flatAdvertDTO);
                return View(flatAdvertModel);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }
    }
}
