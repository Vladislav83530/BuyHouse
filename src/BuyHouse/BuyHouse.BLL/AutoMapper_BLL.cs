using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;

namespace BuyHouse.BLL
{
    public class AutoMapper_BLL <T,K> where T : class where K : class
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<FlatAdvertDTO, FlatAdvert>();
                config.CreateMap<FlatAdvert, FlatAdvertDTO>();

                config.CreateMap<HouseAdvertDTO, HouseAdvert>();
                config.CreateMap<HouseAdvert, HouseAdvertDTO>();

                config.CreateMap<RoomAdvertDTO, RoomAdvert>();
                config.CreateMap<RoomAdvert, RoomAdvertDTO>();

                config.CreateMap<RealtyMainInfoDTO, RealtyMainInfo>();
                config.CreateMap<RealtyMainInfo, RealtyMainInfoDTO>();

                config.CreateMap<RealtyPhotoDTO, RealtyPhoto>();
                config.CreateMap<RealtyPhoto, RealtyPhotoDTO>();

                config.CreateMap<T, K>();
                config.CreateMap<K, T>();
            });
        }
    }
}
