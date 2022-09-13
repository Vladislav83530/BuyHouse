using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertService : AdvertService<FlatAdvert>, IFlatAdvertService
    {
        private readonly ApplicationDbContext _context;
        public FlatAdvertService(ApplicationDbContext context, IPhotosService photoService) : base(context, photoService) 
        {
            _context = context;         
        }

        //TODO: pagSize to ResponseDTO
        public async Task<ResponseFlatAdvertDTO> GetFlatAdvertByParameters(string cityName, int page =1)
        {
            int pageSize = 3;
            IQueryable<FlatAdvert> flatAdverts = _context.FlatAdverts.Include(x=>x.ApplicationUser);

            if (!String.IsNullOrEmpty(cityName))
            {
                flatAdverts = flatAdverts.Where(p => p.MainInfo.City.Contains(cityName));
            }

            var count = await flatAdverts.CountAsync();
            var flatAdverts_ = await flatAdverts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ResponseFlatAdvertDTO output = new ResponseFlatAdvertDTO()
            {
                Count = count,
                FlatAdverts = flatAdverts_
            };

            return output;
        }
    }
}
