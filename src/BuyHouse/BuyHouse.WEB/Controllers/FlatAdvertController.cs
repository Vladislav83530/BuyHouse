using BuyHouse.BLL.DTO;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.AspNetCore.Mvc;

namespace BuyHouse.WEB.Controllers
{
    public class FlatAdvertController : Controller
    {
        private readonly IAdvertService<FlatAdvertDTO, FlatAdvert> _flatAdvertService;

        public FlatAdvertController(IAdvertService<FlatAdvertDTO, FlatAdvert> flatAdvertService)
        {
            _flatAdvertService = flatAdvertService;
        }

        //TODO: HttpPos | delete region | currentUserId change | Redirect to action
        [HttpGet]
        public async Task<IActionResult> CreateAdvert(/*FlatAdvertDTO flatAdvert*/)
        {
            #region Test Data
            RealtyPhotoDTO photoDTO1 = new RealtyPhotoDTO() { Name = "room1", Path = "a/b/c/" };
            RealtyPhotoDTO photoDTO2 = new RealtyPhotoDTO() { Name = "room2", Path = "a/b/d/" };
            List<RealtyPhotoDTO> photos = new List<RealtyPhotoDTO>();
            photos.Add(photoDTO1);
            photos.Add(photoDTO2);
            FlatAdvertDTO flatAdvert = new FlatAdvertDTO()
            {
                MainInfo = new RealtyMainInfoDTO()
                {
                    City = "Lviv",
                    Region = "Lviv region",
                    Street = "Some street",
                    FlatNumber = 23,
                    HouseNumber = "21/1",
                    RegistrationDate = DateTime.Today
                },
                Photos = photos,
                Description = "Some main description",
                Type = DAL.Entities.HelperEnum.TypeOfRealty.Secondary,
                Rooms = 4,
                TypeOfWalls = "Some type of walls",
                TotalArea = 200,
                LivingArea = 180,
                Floor = 7,
                FeatureOfLayout = "Some features of layout",
                Heating = DAL.Entities.HelperEnum.Heating.Centralized,
                YearBuilt = 2021,
                RegistrationNumber = "123476489312",
                Price = 32121312,
                Currency = DAL.Entities.HelperEnum.Currency.UAH,
                TypePrice = DAL.Entities.HelperEnum.TypePrice.AllPrice,
                LikeCount = 56
            };
            #endregion

            if (ModelState.IsValid)
            {
                string? currentUserId = "0f8fad5b-d9cb-469f-a165-70867728950e";
                try
                {
                    await _flatAdvertService.Create(flatAdvert, currentUserId);
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
        public async Task<IActionResult> GetFlatAdverts()
        {
            var flatAdverts = await _flatAdvertService.GetAll ();
            return View(flatAdverts);
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
            var flatAdvertDTO = await _flatAdvertService.GetById(flatAdvertId);
            return View(flatAdvertDTO);
        }
    }
}
