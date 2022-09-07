using BuyHouse.BLL.DTO.AdvertDTO;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IFlatAdvertService
    {
        void AddFlatAdvert(FlatAdvertDTO flatAdvert);
        void UpdateFlatAdvert(FlatAdvertDTO flatAdvert);
        void DeleteFlatAdvert(int flatAdvertId);
    }
}
