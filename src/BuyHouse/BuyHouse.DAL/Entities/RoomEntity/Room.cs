using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities.RoomEntity
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public RealtyMainInfo MainInfo { get; set; }
        public RoomParameters Parameters { get; set; }
        public ICollection<RealtyPhotos> Photos {get; set;}
    }
}
