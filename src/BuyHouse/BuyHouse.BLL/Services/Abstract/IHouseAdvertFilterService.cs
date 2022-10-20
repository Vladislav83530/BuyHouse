using BuyHouse.BLL.DTO;
using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IHouseAdvertFilterService
    {
        public Task<ResponseHouseAdvertDTO> GetHouseAdvertByParametersAsync(HouseAdvertFilter filter, int pageSize, int page);
        public Task<IEnumerable<HouseAdvert>> GetMostLikedHouseAdvertAsync();
        public Task<IEnumerable<HouseAdvert>> GetSellersHouseAdvertsAsync(string? currentUserId);
    }
}
