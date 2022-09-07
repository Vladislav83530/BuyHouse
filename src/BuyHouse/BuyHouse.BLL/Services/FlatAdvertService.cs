using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertService : IFlatAdvertService
    {
        private readonly IFlatAdvertRepository _flatAdvertRepository;
        public FlatAdvertService(IFlatAdvertRepository flatAdvertRepository)
        {
            _flatAdvertRepository = flatAdvertRepository;
        }

        public void AddFlatAdvert(FlatAdvertDTO flatAdvert)
        {
            throw new NotImplementedException();
        }

        public void DeleteFlatAdvert(int flatAdvertId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFlatAdvert(FlatAdvertDTO flatAdvert)
        {
            throw new NotImplementedException();
        }
    }
}
