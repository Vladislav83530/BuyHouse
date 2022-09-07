using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.DAL.Repositories
{
    public class FlatAdvertRepository : IFlatAdvertRepository
    {
        private readonly ApplicationDbContext _context;
        public FlatAdvertRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddFlatAdvert(FlatAdvert flatAdvert)
        {
            throw new NotImplementedException();
        }

        public void DeleteFlatAdvert(int flatAdvertId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFlatAdvert(FlatAdvert flatAdvert)
        {
            throw new NotImplementedException();
        }
    }
}
