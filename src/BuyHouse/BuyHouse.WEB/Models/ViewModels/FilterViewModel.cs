using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string cityName)
        {
            SelectedCityName = cityName;
        }
        
        public string SelectedCityName { get; private set;  }
    }
}
