using BuyHouse.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IPhotosService
    {
        public Task<IEnumerable<RealtyPhoto>> AddPhotoToAdvert(IFormFileCollection uploads, string currentUserId);
    }
}
