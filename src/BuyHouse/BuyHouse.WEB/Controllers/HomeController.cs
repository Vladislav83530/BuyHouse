using AutoMapper;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;
using BuyHouse.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuyHouse.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlatAdvertService _flatAdvertService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IFlatAdvertService flatAdvertService, IMapper mapper)
        {
            _logger = logger;
            _flatAdvertService = flatAdvertService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<FlatAdvert> flatAdverts_ =await _flatAdvertService.GetMostLikedFlatAdvert();
                List<FlatAdvertShortModel> flatAdverts = _mapper.Map<IEnumerable<FlatAdvert>, List<FlatAdvertShortModel>>(flatAdverts_);
                HomeIndexViewModel vm = new HomeIndexViewModel
                {
                    FlatAdverts = flatAdverts,
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { exception = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string exception)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = $"Error message: {exception}" });
        }
    }
}