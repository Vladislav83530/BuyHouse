using Microsoft.EntityFrameworkCore;

namespace BuyHouse.DAL.Entities
{
    [Owned]
    public class RealtyPhoto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
    }
}
