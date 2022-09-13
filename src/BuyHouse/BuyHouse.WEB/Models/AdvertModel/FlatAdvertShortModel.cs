using BuyHouse.DAL.Entities.HelperEnum;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class FlatAdvertShortModel
    {
        public int Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Кількість кімнат")]
        public int? Rooms { get; set; }
        public double? TotalArea { get; set; }
        [Required(ErrorMessage = "Поле має бути заповненим")]
        [Display(Name = "Житлова площа")]    
        public int? Floor { get; set; }

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

        /*Info for statistic*/
        public int? LikeCount { get; set; }
    }
}
