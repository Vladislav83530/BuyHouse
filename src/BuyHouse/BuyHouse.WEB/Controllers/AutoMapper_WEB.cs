using AutoMapper;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Controllers
{
    public class AutoMapper_WEB
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<FlatAdvert, FlatAdvertShortModel>();
                config.CreateMap<FlatAdvertShortModel, FlatAdvert>();

                config.CreateMap<FlatAdvert, FlatAdvertModel>();
                config.CreateMap<FlatAdvertModel, FlatAdvert>();

                config.CreateMap<RealtyMainInfo, RealtyMainInfoModel>();
                config.CreateMap<RealtyMainInfoModel, RealtyMainInfo>();

                config.CreateMap<RealtyPhoto, RealtyPhotoModel>();
                config.CreateMap<RealtyPhotoModel, RealtyPhoto>();
            });
        }
    }
}
