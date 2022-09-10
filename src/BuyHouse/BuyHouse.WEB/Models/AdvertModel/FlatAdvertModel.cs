using BuyHouse.DAL.Entities.HelperEnum;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class FlatAdvertModel
    {
        public int Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Поле має бути заповненим")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Тип нерухомості")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Кількість кімнат")]
        public int? Rooms { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Тип стін")]
        public TypeOfWalls? TypeOfWalls { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Загальна площа")]
        public double? TotalArea { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Житлова площа")]
        public double? LivingArea { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Поверх")]
        public int? Floor { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Опалення")]
        public string? Heating { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Рік побудови")]
        public int? YearBuilt { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Реєстраційний номер")]
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Ціна")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Валюта")]
        public Currency? Currency { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Ціна за")]
        public string? TypePrice { get; set; }
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }

        public string? UserID { get; set; }
    }
}
