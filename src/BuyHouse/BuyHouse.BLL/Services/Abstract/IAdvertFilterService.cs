using BuyHouse.BLL.DTO;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IAdvertFilterService<TAdvert, TFilter> 
    {
        public Task<ResponseAdvertDTO<TAdvert>> GetAdvertByParametersAsync(TFilter filter, int pageSize, int page);
        public Task<IEnumerable<TAdvert>> GetMostLikedAdvertAsync();
        public Task<IEnumerable<TAdvert>> GetSellersAdvertsAsync(string? currentUserId);
        public Task<IEnumerable<TAdvert>> GetLikedAdvertsByUserAsync(string? currentUserId);
    }
}
