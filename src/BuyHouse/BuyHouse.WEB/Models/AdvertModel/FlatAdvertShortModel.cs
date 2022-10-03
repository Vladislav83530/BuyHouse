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
        public string? Description { get; set; }
        [Display(Name = "Rooms")]
        public uint Rooms { get; set; }
        [Display(Name = "TotalArea")]
        public double TotalArea { get; set; }
        [Display(Name = "Floor")]
        public uint Floor { get; set; }

        /*Advert properties*/
        [Display(Name = "TotalPrice")]
        public uint TotalPrice { get; set; }
        [Display(Name = "PricePerSquareMeter")]
        public uint PricePerSquareMeter { get; set; }
        [Display(Name = "Currency")]
        public Currency Currency { get; set; }
        [Display(Name = "TypePrice")]
        public TypeOfPrice TypePrice { get; set; }

        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public uint LikeCount { get; set; }
    }
}
