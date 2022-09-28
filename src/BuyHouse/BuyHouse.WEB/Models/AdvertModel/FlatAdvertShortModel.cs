﻿using BuyHouse.DAL.Entities.HelperEnum;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.AdvertModel
{
    public class FlatAdvertShortModel
    {
        public int Id { get; set; }
        public RealtyMainInfoModel? MainInfo { get; set; }
        public ICollection<RealtyPhotoModel>? Photos { get; set; }

        /*Main flat parameters*/
        [Required]
        public uint Rooms { get; set; }
        public double TotalArea { get; set; }
        [Required] 
        public uint Floor { get; set; }

        /*Advert properties*/
        [Required]
        public uint TotalPrice { get; set; }
        public uint PricePerSquareMeter { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public TypeOfPrice TypePrice { get; set; }

        /*Info for statistic*/
        public uint LikeCount { get; set; }
    }
}
