using AutoMapper;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Controllers
{
    public class AutoMapper_API : Profile
    {
        public AutoMapper_API()
        {
            CreateMap<FlatAdvert, FlatAdvertModel>();
            CreateMap<FlatAdvertModel, FlatAdvert>();

            CreateMap<HouseAdvert, HouseAdvertModel>();
            CreateMap<HouseAdvertModel, HouseAdvert>();

            CreateMap<RealtyMainInfo, RealtyMainInfoModel>();
            CreateMap<RealtyMainInfoModel, RealtyMainInfo>();

            CreateMap<RealtyPhoto, RealtyPhotoModel>();
            CreateMap<RealtyPhotoModel, RealtyPhoto>();
        }
    }
}
