using AutoMapper;
using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.BLL.Services
{
    public class FlatAdvertService : GenericAdvertService<FlatAdvertDTO, FlatAdvert>, IAdvertService<FlatAdvertDTO, FlatAdvert>
    {
        private readonly IRepository<FlatAdvert> _flatAdvertRepository;
        private readonly IMapper _mapper;
        public FlatAdvertService(IRepository<FlatAdvert> flatAdvertRepository) : base(flatAdvertRepository)
        {
            _flatAdvertRepository = flatAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<FlatAdvertDTO, FlatAdvert>.GetMapperConfiguration());
        }

        /// <summary>
        /// Create flat advertising
        /// </summary>
        /// <param name="flatAdvert"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(FlatAdvertDTO flatAdvertDTO, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            FlatAdvert flatAdvert = new FlatAdvert();
            flatAdvert = _mapper.Map<FlatAdvertDTO, FlatAdvert>(flatAdvertDTO);
            flatAdvert.UserID = currentUserId;
            flatAdvert.CreationDate = DateTime.Now;

            await _flatAdvertRepository.Create(flatAdvert);
        }
    }
}