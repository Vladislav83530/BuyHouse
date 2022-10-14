using BuyHouse.DAL.Entities.ApplicationUserEntities;
using BuyHouse.DAL.Entities.HelperEnum;

namespace BuyHouse.DAL.Entities.AdvertEntities
{
    public class FlatAdvert
    {
        public int Id { get; set; }
        public RealtyMainInfo? MainInfo { get; set; }
        public ICollection<RealtyPhoto>? Photos { get; set; }

        /*Main flat parameters*/
        public string? Description { get; set; }
        public TypeOfRealty? Type { get; set; }
        public uint? Rooms { get; set; }
        public TypeOfWalls? TypeOfWalls { get; set; }
        public double TotalArea { get; set; }
        public double LivingArea { get; set; }
        public uint Floor { get; set; }
        public uint TotalCountFloors { get; set; }
        public TypeOfHeating Heating { get; set; }
        public uint YearBuilt { get; set; }
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        public ulong TotalPrice { get; set; }
        public ulong PricePerSquareMeter { get; set; }
        public Currency Currency { get; set; }
        public TypeOfPrice TypePrice { get; set; }
        public DateTime CreationDate { get; set; }

        /*Info for statistic*/
        public uint? LikeCount { get; set; }

        public string? UserID { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
