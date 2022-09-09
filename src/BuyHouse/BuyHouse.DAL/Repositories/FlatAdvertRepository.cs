using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.DAL.Repositories
{
    public class FlatAdvertRepository : GenericRepository<FlatAdvert>
    {
        public FlatAdvertRepository(ApplicationDbContext context) : base(context) { }
    }
}