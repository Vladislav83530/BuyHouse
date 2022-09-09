namespace BuyHouse.BLL.Services.Abstract
{
    public interface IAdvertService<TAdvertDTO, TAdvert> : IGeneralAdvertService<TAdvertDTO, TAdvert> 
        where TAdvertDTO : class 
        where TAdvert : class
    {
        public Task Create(TAdvertDTO flatAdvert, string? currentUserId);
    }
}