using BuyHouse.BLL.DTO;
using BuyHouse.DAL.Entities.AdvertEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IFlatAdvertService
    {
        public Task<ResponseFlatAdvertDTO> GetFlatAdvertByParameters(FlatAdvertFilter filter, int pageSize, int page);
        public Task<IEnumerable<FlatAdvert>> GetMostLikedFlatAdvert();
        public Task<IEnumerable<FlatAdvert>> GetSellersFlatAdverts(string? currentUserId);
    }
}
