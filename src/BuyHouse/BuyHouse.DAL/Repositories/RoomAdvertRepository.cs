using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.DAL.Repositories
{
    public class RoomAdvertRepository : GenericRepository<RoomAdvert>
    {
        public RoomAdvertRepository(ApplicationDbContext context) : base(context) { }
    }
}