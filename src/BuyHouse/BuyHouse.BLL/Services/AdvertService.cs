using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class AdvertService<TAdvert> : IAdvertService<TAdvert> where TAdvert : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotosService _photosService;
        private readonly DbSet<TAdvert> _dbSet;

        public AdvertService(ApplicationDbContext context, IPhotosService photosService)
        {
            _context = context;
            _dbSet = context.Set<TAdvert>();
            _photosService = photosService;
        }

        public async Task<TAdvert> FindAdvertByIdAsync(int? id)
        {
            if (id != null)
            {
                TAdvert? result = await _dbSet.FindAsync(id);
                if (result != null)
                    return result;
                throw new ArgumentNullException(nameof(result));
            }
            throw new ArgumentNullException(nameof(id));
        }

        public async Task<TAdvert> CreateAdvertAsync(TAdvert advert, IFormFileCollection uploads, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            IEnumerable<RealtyPhoto> photos = await _photosService.AddPhotoToAdvert(uploads, currentUserId);
            if (advert != null)
            {
                if (advert is FlatAdvert)
                {            
                    FlatAdvert flatAdvert = (advert as FlatAdvert);
                    flatAdvert.UserID = currentUserId;
                    flatAdvert.CreationDate = DateTime.Now;
                    flatAdvert.Photos =(ICollection<RealtyPhoto>)photos;
                    if(flatAdvert.TypePrice == "за об'єкт")
                        flatAdvert.PricePerSquareMeter = (int)(flatAdvert.TotalPrice / flatAdvert.TotalArea);
                    else
                    {
                        flatAdvert.PricePerSquareMeter = flatAdvert.TotalPrice;
                        flatAdvert.TotalPrice = (int)(flatAdvert.TotalPrice * flatAdvert.TotalArea);
                    }
                }
                if (advert is HouseAdvert)
                {
                    HouseAdvert houseAdvert = (advert as HouseAdvert);
                    houseAdvert.UserID = currentUserId;
                    houseAdvert.CreationDate = DateTime.Now;
                    houseAdvert.Photos = (ICollection<RealtyPhoto>)photos;
                    if (houseAdvert.TypePrice == "за об'єкт")
                        houseAdvert.PricePerSquareMeter = (int)(houseAdvert.TotalPrice / houseAdvert.TotalArea);
                    else
                    {
                        houseAdvert.PricePerSquareMeter = houseAdvert.TotalPrice;
                        houseAdvert.TotalPrice = (int)(houseAdvert.TotalPrice * houseAdvert.TotalArea);
                    }
                }
                if (advert is RoomAdvert)
                {
                    RoomAdvert roomAdvert = (advert as RoomAdvert);
                    roomAdvert.UserID = currentUserId;
                    roomAdvert.CreationDate = DateTime.Now;
                    roomAdvert.Photos = (ICollection<RealtyPhoto>)photos;
                    if (roomAdvert.TypePrice == "за об'єкт")
                        roomAdvert.PricePerSquareMeter = (int)(roomAdvert.TotalPrice / roomAdvert.TotalArea);
                    else
                    {
                        roomAdvert.PricePerSquareMeter = roomAdvert.TotalPrice;
                        roomAdvert.TotalPrice = (int)(roomAdvert.TotalPrice * roomAdvert.TotalArea);
                    }
                }
                await _dbSet.AddAsync(advert);
                await _context.SaveChangesAsync();
                return advert;
            }
            throw new NullReferenceException(nameof(advert));
        }
    }
}
