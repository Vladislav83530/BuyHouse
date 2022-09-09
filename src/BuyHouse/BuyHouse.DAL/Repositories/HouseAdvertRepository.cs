using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.DAL.Repositories
{
    public class HouseAdvertRepository : GenericRepository<HouseAdvert>
    {
        public HouseAdvertRepository(ApplicationDbContext context) : base(context) { }
    }
}