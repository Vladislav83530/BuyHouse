using BuyHouse.DAL.Entities.AdvertEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.DTO
{
    public class ResponseFlatAdvertDTO
    {
        public IEnumerable<FlatAdvert> FlatAdverts {get ;set;}
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}
