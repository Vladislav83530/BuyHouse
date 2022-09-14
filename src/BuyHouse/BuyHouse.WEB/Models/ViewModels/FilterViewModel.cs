using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class FilterViewModel
    {
        public string SelectedCityName { get;  set;  }
        public SelectList CountRooms { get;  set; }
        public int SelectedMinPrice { get; set; }
        public int SelectedMaxPrice { get; set; }
        public SelectList Currency { get; set; } 
        public SelectList TypeOfPrice { get; set; }
        public double SelectedMinTotalArea { get; set; }
        public double SelectedMaxTotalArea { get; set; }
        public int SelecetedMinFloor { get; set; }
        public int SelectedMaxFloor { get; set; }
    }
}
