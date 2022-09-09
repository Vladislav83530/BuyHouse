using AutoMapper;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.BLL.Services
{
    public class RoomAdvertService : GenericAdvertService<RoomAdvertDTO, RoomAdvert>, IAdvertService<RoomAdvertDTO, RoomAdvert>
    {
        private readonly IRepository<RoomAdvert> _roomAdvertRepository;
        private readonly IMapper _mapper;
        public RoomAdvertService(IRepository<RoomAdvert> roomAdvertRepository) : base(roomAdvertRepository)
        {
            _roomAdvertRepository = roomAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<RoomAdvert, RoomAdvert>.GetMapperConfiguration());
        }

        /// <summary>
        /// Create room advertising
        /// </summary>
        /// <param name="flatAdvertDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(RoomAdvertDTO roomAdvertDTO, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            RoomAdvert roomAdvert = new RoomAdvert();
            roomAdvert = _mapper.Map<RoomAdvertDTO, RoomAdvert>(roomAdvertDTO);
            roomAdvert.UserID = currentUserId;
            roomAdvert.CreationDate = DateTime.Now;

            await _roomAdvertRepository.Create(roomAdvert);
        }
    }
}
