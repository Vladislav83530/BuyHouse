using BuyHouse.BLL.DTO;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class GetHouseAdvertViewModel
    {
        public HouseAdvertModel? HouseAdvert { get; set; }
        public UserProfileDTO? UserProfile { get; set; }
    }
}
