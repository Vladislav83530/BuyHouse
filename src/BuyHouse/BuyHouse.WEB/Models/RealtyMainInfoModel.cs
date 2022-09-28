using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models
{
    public class RealtyMainInfoModel
    {
        [Required]
        public string? Region { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? HouseNumber { get; set; }
        public uint FlatNumber { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
