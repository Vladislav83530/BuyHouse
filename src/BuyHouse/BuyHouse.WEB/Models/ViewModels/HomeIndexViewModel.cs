using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<FlatAdvertShortModel> FlatAdverts { get; set; }
        public IEnumerable<HouseAdvertShortModel> HouseAdverts { get; set; }
        public IEnumerable<RoomAdvertShortModel> RoomAdverts { get; set; }
    }
}
