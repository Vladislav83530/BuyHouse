using BuyHouse.DAL.Entities.HelperEnum;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class HouseAdvertModel
    {
        public int Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        [Display(Name = "Опис")]
        public string? Description { get; set; }
        [Display(Name = "Тип")]
        public string? Type { get; set; }
        [Display(Name ="Кількість кімнат")]
        public int? Rooms { get; set; }
        [Display(Name = "Тип стін")]
        public string? TypeOfWalls { get; set; }
        [Display(Name = "Загальна площа")]
        public double? TotalArea { get; set; }
        [Display(Name = "Площа ділянки")]
        public double? LandArea { get; set; }
        [Display(Name = "Житлова площа")]
        public double? LivingArea { get; set; }
        [Display(Name = "Поверх")]
        public int? Floor { get; set; }
        [Display(Name = "Опалення")]
        public string? Heating { get; set; }
        [Display(Name = "Рік побудови")]
        public int? YearBuilt { get; set; }
        [Display(Name = "Реєстраційний номер")]
        public string? RegistrationNumber { get; set; }
        [Display(Name = "Кадастровий номер")]
        public string? CadastralNumber { get; set; }

        /*Advert properties*/
        [Display(Name = "Ціна")]
        public double? Price { get; set; }
        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }
        [Display(Name = "Ціна за")]
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }

        public string? UserID { get; set; }
    }
}
