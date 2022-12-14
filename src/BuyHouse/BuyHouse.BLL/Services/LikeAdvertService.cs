using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public async Task<uint> LikeFlatAdvertAsync(int flatAdvertId, string currentUserId)
        {
            FlatAdvert flatAdvert = await _context.FlatAdverts.FindAsync(flatAdvertId);
            bool isLiked = false;
            if (!String.IsNullOrEmpty(currentUserId))
            {
                var flatAdvertLike_ = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == flatAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.FlatAdvert && x.UserId == currentUserId);
                if (flatAdvertLike_ == null)
                {
                    flatAdvert.LikeCount += 1;
                    _context.Likes.Add(new Like { AdvertId = flatAdvertId, UserId = currentUserId, TypeOfRealty = TypeOfRealtyAdvert.FlatAdvert });
                    isLiked = true;
                }
                else
                {
                    flatAdvert.LikeCount -= 1;
                    _context.Likes.Remove(flatAdvertLike_);
                    isLiked = false;
                }
                await _context.SaveChangesAsync();
                return  flatAdvert.LikeCount;
            }
            return  flatAdvert.LikeCount ;
        }

        /// <summary>
        /// Like house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Count of likes</returns>
        public async Task<uint> LikeHouseAdvertAsync(int houseAdvertId, string currentUserId)
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
        public async Task<uint> LikeRoomAdvertAsync(int roomAdvertId, string currentUserId)
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

        /// <summary>
        /// Get realty advert IDs
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="typeOfRealtyAdvert"></param>
        /// <returns>liked advert IDs</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<int>> GetLikedAdvertIdsAsync(string currentUserId, TypeOfRealtyAdvert typeOfRealtyAdvert)
        {
            if (typeOfRealtyAdvert != null)
            {
                if (typeOfRealtyAdvert == TypeOfRealtyAdvert.FlatAdvert)
                {
                    IEnumerable<int> likedAdvertIds = await _context.Likes.Where(x => x.UserId == currentUserId && x.TypeOfRealty == typeOfRealtyAdvert)
                        .Select(y => y.AdvertId).ToListAsync();
                    return likedAdvertIds;
                }
                if (typeOfRealtyAdvert == TypeOfRealtyAdvert.HouseAdvert)
                {
                    IEnumerable<int> likedAdvertIds = await _context.Likes.Where(x => x.UserId == currentUserId && x.TypeOfRealty == typeOfRealtyAdvert)
                        .Select(y => y.AdvertId).ToListAsync();
                    return likedAdvertIds;
                }
                if (typeOfRealtyAdvert == TypeOfRealtyAdvert.RoomAdvert)
                {
                    IEnumerable<int> likedAdvertIds = await _context.Likes.Where(x => x.UserId == currentUserId && x.TypeOfRealty == typeOfRealtyAdvert)
                        .Select(y => y.AdvertId).ToListAsync();
                    return likedAdvertIds;
                }
            }
            throw new Exception("There is no such type of realty advert");         
        }

        /// <summary>
        /// Dislike flat advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task DislikeFlatAdvertAsync(int flatAdvertId, string currentUserId)
        {
            var flatAdvert = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == flatAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.FlatAdvert && x.UserId == currentUserId);
            _context.Likes.Remove(flatAdvert);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dislike house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task DislikeHouseAdvertAsync(int houseAdvertId, string currentUserId)
        {
            var houseAdvert = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == houseAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.HouseAdvert && x.UserId == currentUserId);
            _context.Likes.Remove(houseAdvert);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dislike room advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task DislikeRoomAdvertAsync(int roomAdvertId, string currentUserId)
        {
            var roomAdvert = await _context.Likes.FirstOrDefaultAsync(x => x.AdvertId == roomAdvertId && x.TypeOfRealty == TypeOfRealtyAdvert.RoomAdvert && x.UserId == currentUserId);
            _context.Likes.Remove(roomAdvert);
            await _context.SaveChangesAsync();
        }
    }
}
