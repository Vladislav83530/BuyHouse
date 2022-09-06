using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities.RoomEntity
{
    public class RoomParameters
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double TotalArea { get; set; }
        public double LivingArea { get; set; }
        public int Floor { get; set; }
        public string Heating { get; set; }
        public string RegistrationNumber { get; set; }


        [ForeignKey("RoomID")]
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}
