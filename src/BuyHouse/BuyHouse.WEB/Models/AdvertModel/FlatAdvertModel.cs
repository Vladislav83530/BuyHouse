using BuyHouse.DAL.Entities.HelperEnum;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class FlatAdvertModel
    {
        public int Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int Rooms { get; set; }
        public TypeOfWalls TypeOfWalls { get; set; }
        public double TotalArea { get; set; }
        public double LivingArea { get; set; }
        public int Floor { get; set; }
        public string? Heating { get; set; }
        public int YearBuilt { get; set; }
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        public int Price { get; set; }
        public Currency Currency { get; set; }
        public string? TypePrice { get; set; }
        public DateTime CreationDate { get; set; }

        /*Info for statistic*/
        public int LikeCount { get; set; }
    }
}
