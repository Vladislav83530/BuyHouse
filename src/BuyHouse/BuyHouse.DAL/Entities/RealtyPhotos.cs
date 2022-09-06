using BuyHouse.DAL.Entities.FlatEntity;
using BuyHouse.DAL.Entities.RoomEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities
{
    public class RealtyPhotos
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        [ForeignKey("FlatID")]
        public int FlatID { get; set; }
        public Flat Flat { get; set; }
    }
}
