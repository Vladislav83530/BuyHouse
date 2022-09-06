using BuyHouse.DAL.Entities.ApplicationUserEntity;
using BuyHouse.DAL.Entities.FlatEntity;
using BuyHouse.DAL.Entities.RoomEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities
{
    public class Advert
    {
        [Key]
        public int AdvertID { get; set; }
        public int AdvertIDForRoom { get; set; }
        public string Rubric { get; set; } // maybe enum ? //public TypeOfRealEstate {get; set;}
        public Flat Flat { get; set; }
        public Room Room { get; set; }

        /*Add entity House*/
        //public BuyHouse House { get; set; }
        public double Price { get; set; }
        //public Currency Currency { get; set; }
        //public TypePrice TypePrice { get; set; }
        public DateTime CreationDate { get; set; }
        public int LikeCount { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
