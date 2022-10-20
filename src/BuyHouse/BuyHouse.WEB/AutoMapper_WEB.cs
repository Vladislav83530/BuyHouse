using AutoMapper;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB
{
    public class AutoMapper_WEB : Profile
    {
        public AutoMapper_WEB()
        {
            CreateMap<FlatAdvert, FlatAdvertShortModel>();
            CreateMap<FlatAdvertShortModel, FlatAdvert>();

            CreateMap<FlatAdvert, FlatAdvertModel>();
            CreateMap<FlatAdvertModel, FlatAdvert>();

            CreateMap<HouseAdvert, HouseAdvertModel>();
            CreateMap<HouseAdvertModel, HouseAdvert>();

            CreateMap<HouseAdvert, HouseAdvertShortModel>();
            CreateMap<HouseAdvertShortModel, HouseAdvert>();

            CreateMap<RealtyMainInfo, RealtyMainInfoModel>();
            CreateMap<RealtyMainInfoModel, RealtyMainInfo>();

            CreateMap<RealtyPhoto, RealtyPhotoModel>();
            CreateMap<RealtyPhotoModel, RealtyPhoto>();
        }
    }
}
