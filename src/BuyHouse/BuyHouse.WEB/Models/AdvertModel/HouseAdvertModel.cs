using BuyHouse.DAL.Entities.HelperEnum;
using BuyHouse.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class HouseAdvertModel
    {
        public int? Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main house parameters*/
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Type")]
        public TypeOfRealty? Type { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Rooms")]
        [Range(0, uint.MaxValue, ErrorMessage = "RangeError")]
        public uint Rooms { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TypeOfWalls")]
        public TypeOfWalls? TypeOfWalls { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalArea")]
        [Range(0, double.MaxValue, ErrorMessage = "RangeError")]
        public double TotalArea { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LandArea")]
        [Range(0, double.MaxValue, ErrorMessage = "RangeError")]
        public double LandArea { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LivingArea")]
        [Range(0, double.MaxValue, ErrorMessage = "RangeError")]
        public double LivingArea { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalCountFloors")]
        [Range(0, uint.MaxValue, ErrorMessage = "RangeError")]
        public uint TotalCountFloors { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Heating")]
        public TypeOfHeating Heating { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "YearBuilt")]
        public uint YearBuilt { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "RegistrationNumber")]
        [RegularExpression(@"[1-9]{1}[0-9]{12}", ErrorMessage = "RegistrationNumberError")]
        public string? RegistrationNumber { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "CadastralNumber")]
        [RegularExpression(@"[0-9]{10}:[0-9]{2}:[0-9]{3}:[0-9]{4}", ErrorMessage = "CadastralNumberError")]
        public string? CadastralNumber { get; set; }

        /*Advert properties*/
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TotalPrice")]
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
