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
    public class RoomAdvertService : GenericAdvertService<RoomAdvertDTO, RoomAdvert>, IAdvertService<RoomAdvertDTO, RoomAdvert>
    {
        private readonly IRepository<RoomAdvert> _roomAdvertRepository;
        private readonly IPhotosService _photosService;
        private readonly IMapper _mapper;
        public RoomAdvertService(IRepository<RoomAdvert> roomAdvertRepository, IPhotosService photosService) : base(roomAdvertRepository)
        {
            _roomAdvertRepository = roomAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<RoomAdvert, RoomAdvert>.GetMapperConfiguration());
            _photosService = photosService;
        }

        /// <summary>
        /// Create room advertising
        /// </summary>
        /// <param name="flatAdvertDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(RoomAdvertDTO roomAdvertDTO, IFormFileCollection uploads, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            RoomAdvert roomAdvert = new RoomAdvert();
            List<RealtyPhotoDTO> photos = new List<RealtyPhotoDTO>();
            photos = await _photosService.AddPhoto(uploads, currentUserId);

            roomAdvert = _mapper.Map<RoomAdvertDTO, RoomAdvert>(roomAdvertDTO);
            roomAdvert.UserID = currentUserId;
            roomAdvert.CreationDate = DateTime.Now;
            roomAdvert.Photos = _mapper.Map<IEnumerable<RealtyPhotoDTO>, List<RealtyPhoto>>(photos);

            await _roomAdvertRepository.Create(roomAdvert);
        }
    }
}
