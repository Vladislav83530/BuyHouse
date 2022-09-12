using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.DTO.AdvertDTO;
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
                config.CreateMap<FlatAdvertDTO, FlatAdvertModel>();
                config.CreateMap<FlatAdvertModel, FlatAdvertDTO>();

                config.CreateMap<HouseAdvertDTO, HouseAdvertModel>();
                config.CreateMap<HouseAdvertModel, HouseAdvertDTO>();

                config.CreateMap<RealtyMainInfoDTO, RealtyMainInfoModel>();
                config.CreateMap<RealtyMainInfoModel, RealtyMainInfoDTO>();

                config.CreateMap<RealtyPhotoDTO, RealtyPhotoModel>();
                config.CreateMap<RealtyPhotoModel, RealtyPhotoDTO>();
            });
        }
    }
}
