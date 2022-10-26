using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IPhotosService
    {
        public Task<ICollection<RealtyPhoto>> AddPhotoToAdvertAsync(IFormFileCollection uploads, string currentUserId);
        public Task UpdateUserAvatarPhotoAsync(IFormFile uploadedFile, UserAvatar currentUsersAvatar, string currentUserId);
        public Task<FlatAdvert> DeletePhotoFromFlatAdvertAsync(string currentUserId, int flatAdvertId, int photoId);
        public Task<HouseAdvert> DeletePhotoFromHouseAdvertAsync(string currentUserId, int houseAdvertId, int photoId);
        public Task<RoomAdvert> DeletePhotoFromRoomAdvertAsync(string currentUserId, int houseAdvertId, int photoId);
    }
}
