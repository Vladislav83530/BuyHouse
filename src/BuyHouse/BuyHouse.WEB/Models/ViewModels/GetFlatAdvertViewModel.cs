using BuyHouse.BLL.DTO;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class GetFlatAdvertViewModel
    {
        public FlatAdvertModel? FlatAdvert { get; set; }
        public UserProfileDTO? UserProfile { get; set; }
    }
}
