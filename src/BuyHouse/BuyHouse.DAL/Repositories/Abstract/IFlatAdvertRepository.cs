using BuyHouse.DAL.Entities.AdvertEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.DAL.Repositories.Abstract
{
    public interface IFlatAdvertRepository
    {
        void AddFlatAdvert(FlatAdvert flatAdvert);
        void UpdateFlatAdvert(FlatAdvert flatAdvert);
        void DeleteFlatAdvert(int flatAdvertId);
    }
}
