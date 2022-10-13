using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IPhotosService
    {
        public Task<IEnumerable<RealtyPhoto>> AddPhotoToAdvertAsync(IFormFileCollection uploads, string currentUserId);
        public Task UpdateUserAvatarPhotoAsync(IFormFile uploadedFile, UserAvatar currentUsersAvatar, string currentUserId);
        public Task DeletePhotoFromAdvertAsync(string currentUserId, int avertId, int photoId);
    }
}
