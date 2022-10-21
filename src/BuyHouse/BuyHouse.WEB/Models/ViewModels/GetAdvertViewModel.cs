using BuyHouse.BLL.DTO;

namespace BuyHouse.WEB.Models.ViewModels
{
    public class GetAdvertViewModel<T>
    {
        public T? Advert { get; set; }
        public UserProfileDTO? UserProfile { get; set; }
    }
}
