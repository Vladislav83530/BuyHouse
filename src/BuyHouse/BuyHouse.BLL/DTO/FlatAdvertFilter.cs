using BuyHouse.DAL.Entities.HelperEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.DTO
{
    public class FlatAdvertFilter
    {
        public string CityName { get; set; }
        public string CountRooms { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Currency Currency { get; set; }
        public TypeOfPrice TypeOfPrice { get; set; }
        public double MinTotalArea { get; set; }
        public double MaxTotalArea { get; set; }
        public int MinFloor { get; set; }
        public int MaxFloor { get; set; }
    }
}
