using BuyHouse.BLL.DTO;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class IndexFilterViewModel
    {
        public IEnumerable<FlatAdvertShortModel> FlatAdverts { get; set; }
        public FlatAdvertFilter FlatAdvertFilter { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
