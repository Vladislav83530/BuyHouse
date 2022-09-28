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
        public uint Rooms { get; set; }
        [Required]
        public TypeOfWalls TypeOfWalls { get; set; }
        [Required]
        public double TotalArea { get; set; }
        [Required]
        public double LivingArea { get; set; }
        [Required]
        public uint Floor { get; set; }
        [Required]
        public string? Heating { get; set; }
        [Required]
        [Range(1990, 2026)]
        public uint YearBuilt { get; set; }
        [Required]
        [RegularExpression(@"[1-9]{1}[0-9]{12}")]
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        [Required]
        public ulong? TotalPrice { get; set; }
        public ulong? PricePerSquareMeter { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public TypeOfPrice? TypePrice { get; set; }
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public uint? LikeCount { get; set; }
    }
}
