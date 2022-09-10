using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models
{
    public class RealtyMainInfoModel
    {
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name ="Регіон")]
        public string? Region { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Місто")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Вулиця")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Номер будинку")]
        public string? HouseNumber { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Номер квартири")]
        public int? FlatNumber { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Дата реєстрації")]
        public DateTime? RegistrationDate { get; set; }
    }
}
