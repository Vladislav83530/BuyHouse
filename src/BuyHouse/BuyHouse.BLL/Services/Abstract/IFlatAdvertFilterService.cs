using BuyHouse.BLL.DTO;
using BuyHouse.DAL.Entities.AdvertEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IFlatAdvertFilterService
    {
        public Task<ResponseFlatAdvertDTO> GetFlatAdvertByParametersAsync(FlatAdvertFilter filter, int pageSize, int page);
        public Task<IEnumerable<FlatAdvert>> GetMostLikedFlatAdvertAsync();
        public Task<IEnumerable<FlatAdvert>> GetSellersFlatAdvertsAsync(string? currentUserId);
    }
}
