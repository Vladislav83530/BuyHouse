﻿using BuyHouse.DAL.Entities.HelperEnum;

namespace BuyHouse.BLL.DTO.AdvertDTO
{
    public class FlatAdvertDTO
    {
        public int Id { get; set; }
        public RealtyMainInfoDTO MainInfo { get; set; }
        public ICollection<RealtyPhotoDTO> Photos { get; set; }

        /*Main flat parameters*/
        public string Description { get; set; }
        public TypeOfRealty Type { get; set; }
        public int Rooms { get; set; }
        public string TypeOfWalls { get; set; }
        public double TotalArea { get; set; }
        public double LivingArea { get; set; }
        public int Floor { get; set; }
        public string FeatureOfLayout { get; set; }
        public Heating Heating { get; set; }
        public int YearBuilt { get; set; }
        public string RegistrationNumber { get; set; }

        /*Advert properties*/
        public double Price { get; set; }
        public Currency Currency { get; set; }
        public TypePrice TypePrice { get; set; }
        public DateTime CreationDate { get; set; }

        /*Info for statistic*/
        public int LikeCount { get; set; }

        public string UserID { get; set; }
    }
}
