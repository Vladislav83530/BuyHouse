using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models
{
    public class RealtyMainInfoModel
    {
        [Required(ErrorMessage ="RequiredField")]
        [Display(Name ="Region")]
        public string? Region { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "City")]
        public string? City { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Street")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "HouseNumber")]
        public string? HouseNumber { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "FlatNumber")]
        [Range(0,uint.MaxValue, ErrorMessage = "RangeError")]
        public uint FlatNumber { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
    }
}
