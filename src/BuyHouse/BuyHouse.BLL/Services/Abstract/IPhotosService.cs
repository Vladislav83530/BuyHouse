using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IPhotosService
    {
        public Task<IEnumerable<RealtyPhoto>> AddPhotoToAdvert(IFormFileCollection uploads, string currentUserId);
        public Task UpdateUserAvatarPhoto(IFormFile uploadedFile, UserAvatar currentUsersAvatar, string currentUserId);
    }
}
