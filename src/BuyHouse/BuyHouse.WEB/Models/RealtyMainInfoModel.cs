using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models
{
    public class RealtyMainInfoModel
    {
        [Required]
        [Display(Name ="Регіон")]
        public string? Region { get; set; }
        [Required]
        [Display(Name = "Місто")]
        public string? City { get; set; }
        [Required]
        [Display(Name = "Вулиця")]
        public string? Street { get; set; }
        [Required]
        [Display(Name = "Номер будинку")]
        public string? HouseNumber { get; set; }
        [Display(Name = "Номер квартири")]
        public int? FlatNumber { get; set; }
        [Required]
        [Display(Name = "Дата реєстрації")]
        public DateTime? RegistrationDate { get; set; }
    }
}
