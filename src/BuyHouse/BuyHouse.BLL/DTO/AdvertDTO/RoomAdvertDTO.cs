using BuyHouse.DAL.Entities.HelperEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.DTO.AdvertDTO
{
    public class RoomAdvertDTO
    {
        public int Id { get; set; }
        public RealtyMainInfoDTO? MainInfo { get; set; }
        public ICollection<RealtyPhotoDTO>? Photos { get; set; }

        /*Main room parameters*/
        public string? Type { get; set; }
        public string? Description { get; set; }
        public double? TotalArea { get; set; }
        public double? LivingArea { get; set; }
        public int? Floor { get; set; }
        public Heating? Heating { get; set; }
        //[RegularExpression(@"[1-9]\d{1}[0-9]\d{10-12}")]
        public string? RegistrationNumber { get; set; }

        /*Advert properties*/
        public double? Price { get; set; }
        public Currency Currency { get; set; }
        public TypePrice TypePrice { get; set; }
        public DateTime? CreationDate { get; set; }

        /*Info for statistic*/
        public int? LikeCount { get; set; }

        public string UserID { get; set; }
    }
}
