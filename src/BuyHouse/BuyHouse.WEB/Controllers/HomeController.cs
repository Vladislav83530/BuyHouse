using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuyHouse.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertFilterService<FlatAdvert, FlatAdvertFilter> _flatAdvertService;
        private readonly IAdvertFilterService<HouseAdvert, HouseAdvertFilter> _houseAdvertFilterService;
        private readonly IAdvertFilterService<RoomAdvert, RoomAdvertFilter> _roomAdvertFilterService;

        private readonly IMapper _mapper;

        public HomeController(IAdvertFilterService<FlatAdvert, FlatAdvertFilter> flatAdvertService, 
            IMapper mapper, 
            IAdvertFilterService<HouseAdvert, HouseAdvertFilter> houseAdvertFilterService,
            IAdvertFilterService<RoomAdvert, RoomAdvertFilter> roomAdvertFilterService)
        {
            _flatAdvertService = flatAdvertService;
            _mapper = mapper;
            _houseAdvertFilterService = houseAdvertFilterService;
            _roomAdvertFilterService = roomAdvertFilterService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<FlatAdvert> flatAdverts_ = await _flatAdvertService.GetMostLikedAdvertAsync();
                List<FlatAdvertShortModel> flatAdverts = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(flatAdverts_);

                IEnumerable<HouseAdvert> houseAdverts_ = await _houseAdvertFilterService.GetMostLikedAdvertAsync();
                List<HouseAdvertShortModel> houseAdverts = _mapper.Map<IEnumerable<HouseAdvert>, List<HouseAdvertShortModel>>(houseAdverts_);

                IEnumerable<RoomAdvert> roomAdverts_ = await _roomAdvertFilterService.GetMostLikedAdvertAsync();
                List<RoomAdvertShortModel> roomAdverts = _mapper.Map<IEnumerable<RoomAdvert>, List<RoomAdvertShortModel>>(roomAdverts_);

                HomeIndexViewModel vm = new HomeIndexViewModel
                {
                    FlatAdverts = flatAdverts,
                    HouseAdverts =houseAdverts,
                    RoomAdverts = roomAdverts
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string exception)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = $"Error message: {exception}" });
        }
    }
}