using BuyHouse.DAL.Entities.ApplicationUserEntities;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.DAL.Entities.AdvertEntities
{
    public class HouseAdvert
    {
        public int Id { get; set; }
        public RealtyMainInfo? MainInfo { get; set; }
        public ICollection<RealtyPhoto>? Photos { get; set; }

        /*Main flat parameters*/
        public string? Description { get; set; }
        public TypeOfRealty? Type { get; set; }
        public int? Rooms { get; set; }
        public string? TypeOfWalls { get; set; }
        public double? TotalArea { get; set; }
        public double? LandArea { get; set; }
        public double? LivingArea { get; set; }
        public int? Floor { get; set; }
        public Heating? Heating { get; set; }
        [Range(1900, 2026)]
        public int? YearBuilt { get; set; }
        [RegularExpression(@"[1-9]\d{1}[0-9]\d{10-12}")]
        public string? RegistrationNumber { get; set; }
        [RegularExpression(@"\d{1,2}:\d{1,2}:\d{6,7}:\d{1,4}")]
        public string? CadastralNumber { get; set; }

        /*Advert properties*/
        public double? Price { get; set; }
        public Currency Currency { get; set; }
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }

        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
