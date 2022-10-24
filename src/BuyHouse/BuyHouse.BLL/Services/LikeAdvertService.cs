using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.BLL.Services
{
    public class LikeAdvertService : ILikeAdvertService
    {
        private readonly ApplicationDbContext _context;

        public LikeAdvertService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Like flat advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>count of likes</returns>
        public async Task<uint> LikeFlatAdvert(int flatAdvertId, string currentUserId)
        {
            FlatAdvert flatAdvert = await _context.FlatAdverts.FindAsync(flatAdvertId);
            if (!String.IsNullOrEmpty(currentUserId))
            {
                var flatAdvertLike_ = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == flatAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.FlatAdvert && x.UserId == currentUserId);
                if (flatAdvertLike_ == null)
                {
                    flatAdvert.LikeCount += 1;
                    _context.Likes.Add(new Like { AdvertId = flatAdvertId, UserId = currentUserId, TypeOfRealty = TypeOfRealtyAdvert.FlatAdvert });
                }
                else
                {
                    flatAdvert.LikeCount -= 1;
                    _context.Likes.Remove(flatAdvertLike_);
                }
                await _context.SaveChangesAsync();
                return flatAdvert.LikeCount;
            }
            return flatAdvert.LikeCount;
        }

        /// <summary>
        /// Like house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Count of likes</returns>
        public async Task<uint> LikeHouseAdvert(int houseAdvertId, string currentUserId)
        {
            HouseAdvert houseAdvert = await _context.HouseAdverts.FindAsync(houseAdvertId);
            if (!String.IsNullOrEmpty(currentUserId))
            {
                var houseAdvertLike_ = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == houseAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.HouseAdvert && x.UserId == currentUserId);
                if (houseAdvertLike_ == null)
                {
                    houseAdvert.LikeCount += 1;
                    _context.Likes.Add(new Like { AdvertId = houseAdvertId, UserId = currentUserId, TypeOfRealty = TypeOfRealtyAdvert.HouseAdvert });
                }
                else
                {
                    houseAdvert.LikeCount -= 1;
                    _context.Likes.Remove(houseAdvertLike_);
                }
                await _context.SaveChangesAsync();
                return houseAdvert.LikeCount;
            }
            return houseAdvert.LikeCount;
        }

        /// <summary>
        /// Like room advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Count of likes</returns>
        public async Task<uint> LikeRoomAdvert(int roomAdvertId, string currentUserId)
        {
            RoomAdvert roomAdvert = await _context.RoomAdverts.FindAsync(roomAdvertId);
            if (!String.IsNullOrEmpty(currentUserId))
            {
                var roomAdvertLike_ = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == roomAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.RoomAdvert && x.UserId == currentUserId);
                if (roomAdvertLike_ == null)
                {
                    roomAdvert.LikeCount += 1;
                    _context.Likes.Add(new Like { AdvertId = roomAdvertId, UserId = currentUserId, TypeOfRealty = TypeOfRealtyAdvert.RoomAdvert });
                }
                else
                {
                    roomAdvert.LikeCount -= 1;
                    _context.Likes.Remove(roomAdvertLike_);
                }
                await _context.SaveChangesAsync();
                return roomAdvert.LikeCount;
            }
            return roomAdvert.LikeCount;
        }
    }
}
