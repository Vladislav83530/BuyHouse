using BuyHouse.DAL.Entities.HelperEnum;
using BuyHouse.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class RoomAdvertModel
    {
        public int? Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main room parameters*/
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Type")]
        public TypeOfRealty Type { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalArea")]
        [Range(0, double.MaxValue, ErrorMessage = "RangeError")]
        public double TotalArea { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LivingArea")]
        [Range(0, double.MaxValue, ErrorMessage = "RangeError")]
        public double LivingArea { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Floor")]
        [Range(0, uint.MaxValue, ErrorMessage = "RangeError")]
        public uint Floor { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalCountFloors")]
        [Range(0, uint.MaxValue, ErrorMessage = "RangeError")]
        public uint TotalCountFloors { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Heating")]
        public TypeOfHeating Heating { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "RegistrationNumber")]
        [RegularExpression(@"[1-9]{1}[0-9]{12}", ErrorMessage = "RegistrationNumberError")]
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalPrice")]
        [Range(0, ulong.MaxValue, ErrorMessage = "RangeError")]
        public ulong TotalPrice { get; set; }
        [Display(Name = "PricePerSquareMeter")]
        [Range(0, ulong.MaxValue, ErrorMessage = "RangeError")]
        public ulong PricePerSquareMeter { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Currency")]
        public Currency Currency { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TypePrice")]
        public TypeOfPrice TypePrice { get; set; }
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }

        /*Info for statistic*/
        public uint LikeCount { get; set; }
    }
}
