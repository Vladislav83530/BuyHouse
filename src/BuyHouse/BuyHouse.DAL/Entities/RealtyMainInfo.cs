using BuyHouse.DAL.Entities.FlatEntity;
using BuyHouse.DAL.Entities.RoomEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities
{
    public class RealtyMainInfo
    {
        [Key]
        public int Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public DateTime RegistrationDate { get; set; }


        [ForeignKey("FlatID")]
        public int FlatID { get; set; }
        public Flat Flat { get; set; }

    }
}
