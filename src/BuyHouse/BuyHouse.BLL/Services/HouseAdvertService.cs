using AutoMapper;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.BLL.Services
{
    public class HouseAdvertService : GenericAdvertService<HouseAdvertDTO, HouseAdvert>, IAdvertService<HouseAdvertDTO, HouseAdvert>
    {
        private readonly IRepository<HouseAdvert> _houseAdvertRepository;
        private readonly IMapper _mapper;

        public HouseAdvertService(IRepository<HouseAdvert> houseAdvertRepository) : base(houseAdvertRepository)
        {
            _houseAdvertRepository = houseAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<HouseAdvertDTO, HouseAdvert>.GetMapperConfiguration());
        }

        /// <summary>
        /// Create house advertising
        /// </summary>
        /// <param name="houseAdvertDTO"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(HouseAdvertDTO houseAdvertDTO, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            HouseAdvert houseAdvert = new HouseAdvert();
            houseAdvert = _mapper.Map<HouseAdvertDTO, HouseAdvert>(houseAdvertDTO);
            houseAdvert.UserID = currentUserId;
            houseAdvert.CreationDate = DateTime.Now;

            await _houseAdvertRepository.Create(houseAdvert);
        }
    }
}