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
        [Required]
        [Display(Name = "Кількість кімнат")]
        public int? Rooms { get; set; }
        public double? TotalArea { get; set; }
        [Required]
        [Display(Name = "Житлова площа")]    
        public int? Floor { get; set; }

        /*Advert properties*/
        [Required]
        [Display(Name = "Ціна")]
        public int? TotalPrice { get; set; }
        public int? PricePerSquareMeter { get; set; }
        [Required]
        [Display(Name = "Валюта")]
        public Currency? Currency { get; set; }
        [Required]
        [Display(Name = "Ціна за")]
        public TypeOfPrice? TypePrice { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }
    }
}
