using BuyHouse.BLL.DTO;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IPhotosService
    {
        public Task<List<RealtyPhotoDTO>> AddPhoto(IFormFileCollection uploads, string currentUserId);
    }
}
