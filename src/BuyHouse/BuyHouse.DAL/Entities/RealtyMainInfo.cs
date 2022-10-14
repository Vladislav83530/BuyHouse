using Microsoft.EntityFrameworkCore;

namespace BuyHouse.DAL.Entities
{
    [Owned]
    public class RealtyMainInfo
    {
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public uint FlatNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
