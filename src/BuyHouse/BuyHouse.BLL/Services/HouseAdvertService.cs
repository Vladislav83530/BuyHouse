using AutoMapper;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace BuyHouse.BLL.Services
{
    public class HouseAdvertService : GenericAdvertService<HouseAdvertDTO, HouseAdvert>, IAdvertService<HouseAdvertDTO, HouseAdvert>
    {
        private readonly IRepository<HouseAdvert> _houseAdvertRepository;
        private readonly IPhotosService _photosService;
        private readonly IMapper _mapper;

        public HouseAdvertService(IRepository<HouseAdvert> houseAdvertRepository, IPhotosService photosService) : base(houseAdvertRepository)
        {
            _houseAdvertRepository = houseAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<HouseAdvertDTO, HouseAdvert>.GetMapperConfiguration());
            _photosService = photosService;
        }

        /// <summary>
        /// Create house advertising
        /// </summary>
        /// <param name="houseAdvertDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(HouseAdvertDTO houseAdvertDTO, IFormFileCollection uploads, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            HouseAdvert houseAdvert = new HouseAdvert();
            List<RealtyPhotoDTO> photos = new List<RealtyPhotoDTO>();
            photos = await _photosService.AddPhoto(uploads, currentUserId);

            houseAdvert = _mapper.Map<HouseAdvertDTO, HouseAdvert>(houseAdvertDTO);
            houseAdvert.UserID = currentUserId;
            houseAdvert.CreationDate = DateTime.Now;
            houseAdvert.Photos = _mapper.Map<IEnumerable<RealtyPhotoDTO>, List<RealtyPhoto>>(photos);

            await _houseAdvertRepository.Create(houseAdvert);
        }
    }
}