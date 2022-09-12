using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IAdvertService<TAdvertDTO, TAdvert> : IGeneralAdvertService<TAdvertDTO, TAdvert> 
        where TAdvertDTO : class 
        where TAdvert : class
    {
        public Task<TAdvertDTO> Create(TAdvertDTO flatAdvert, IFormFileCollection uploads, string? currentUserId);
    }
}