namespace BuyHouse.BLL.Services.Abstract
{
    public interface ILikeAdvertService
    {
        public Task<uint> LikeFlatAdvert(int flatAdvertId, string currentUserId);
        public Task<uint> LikeHouseAdvert(int houseAdvertId, string currentUserId);
        public Task<uint> LikeRoomAdvert(int roomAdvertId, string currentUserId);
    }
}
