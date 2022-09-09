using BuyHouse.BLL.DTO.AdvertDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.DTO.ApplicationUserDTO
{
    public class ApplicationUserDTO : IdentityUser
    {
        public string? UserSurname { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public ICollection<FlatAdvertDTO>? FlatAdverts { get; set; }
        public ICollection<RoomAdvertDTO>? RoomAdverts { get; set; }
        public ICollection<HouseAdvertDTO>? HouseAdverts { get; set; }
        public UserAvatarDTO UserAvatar { get; set; }
    }
}
