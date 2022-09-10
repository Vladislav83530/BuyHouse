namespace BuyHouse.BLL.Services.Abstract
{
    public interface IGeneralAdvertService<TAdvertDTO, TAdvert> 
        where TAdvertDTO : class 
        where TAdvert : class
    {
        public Task<IEnumerable<TAdvertDTO>> GetAll();
        public Task<TAdvertDTO> GetById(int? advertId);
        public Task Delete(int? advertId);
    }
}