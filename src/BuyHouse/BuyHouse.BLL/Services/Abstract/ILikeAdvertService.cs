using BuyHouse.DAL.Entities;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface ILikeAdvertService
    {
        public Task<uint> LikeFlatAdvertAsync(int flatAdvertId, string currentUserId);
        public Task<uint> LikeHouseAdvertAsync(int houseAdvertId, string currentUserId);
        public Task<uint> LikeRoomAdvertAsync(int roomAdvertId, string currentUserId);
        public Task<IEnumerable<int>> GetLikedAdvertIdsAsync(string currentUserId, TypeOfRealtyAdvert typeOfRealtyAdvert);
        public Task DislikeFlatAdvertAsync(int flatAdvertId, string currentUserId);
        public Task DislikeHouseAdvertAsync(int houseAdvertId, string currentUserId);
        public Task DislikeRoomAdvertAsync(int roomAdvertId, string currentUserId);
    }
}
