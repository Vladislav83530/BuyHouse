using BuyHouse.DAL.Entities.HelperEnum;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class FlatAdvertModel
    {
        public int? Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public int Rooms { get; set; }
        [Required]
        public TypeOfWalls TypeOfWalls { get; set; }
        [Required]
        public double TotalArea { get; set; }
        [Required]
        public double LivingArea { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public string? Heating { get; set; }
        [Required]
        public int YearBuilt { get; set; }
        [Required]
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        [Required]
        public int TotalPrice { get; set; }
        public int? PricePerSquareMeter { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public TypeOfPrice? TypePrice { get; set; }
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }
    }
}
