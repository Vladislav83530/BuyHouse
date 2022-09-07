using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.DTO
{
    public class RealtyMainInfoDTO
    {
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
