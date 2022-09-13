using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.Services.Abstract
{
    public interface IAdvertService<TAdvert>
    {
        public Task<TAdvert> FindAdvertByIdAsync(int? id);
        public Task<TAdvert> CreateAdvertAsync(TAdvert advert, IFormFileCollection uploads, string? currentUserId);
    }
}
