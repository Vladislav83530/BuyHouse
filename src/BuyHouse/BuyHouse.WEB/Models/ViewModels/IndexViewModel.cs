using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<FlatAdvertShortModel> FlatAdverts { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
