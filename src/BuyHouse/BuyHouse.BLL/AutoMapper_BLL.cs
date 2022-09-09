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

                config.CreateMap<HouseAdvertDTO, HouseAdvert>();

                config.CreateMap<RealtyMainInfoDTO, RealtyMainInfo>();

                config.CreateMap<RealtyPhotoDTO, RealtyPhoto>();

                config.CreateMap<T, K>();
                config.CreateMap<K, T>();
            });
        }
    }
}
