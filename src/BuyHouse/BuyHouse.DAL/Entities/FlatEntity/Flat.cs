using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities.FlatEntity
{
    public class Flat
    {
        [Key]
        public int FlatID { get; set; }
        public RealtyMainInfo MainInfo { get; set; }
        public FlatParameters Parameters { get; set; }
        public ICollection<RealtyPhotos> Photos { get; set; }

        [ForeignKey("AdvertIDForFlat")]
        public int AdvertID { get; set; }
        public Advert Advert { get; set; }
    }
}
