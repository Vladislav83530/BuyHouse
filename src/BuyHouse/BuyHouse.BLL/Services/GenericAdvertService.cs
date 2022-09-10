using AutoMapper;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Repositories.Abstract;

namespace BuyHouse.BLL.Services
{
    public class GenericAdvertService<TAdvertDTO, TAdvert> : IGeneralAdvertService<TAdvertDTO, TAdvert> 
        where TAdvertDTO : class
        where TAdvert : class
    {
        private readonly IRepository<TAdvert> _advertRepository;
        private readonly IMapper _mapper;

        public GenericAdvertService(IRepository<TAdvert> advertRepository)
        {
            _advertRepository = advertRepository;
            _mapper = new Mapper(AutoMapper_BLL<TAdvertDTO, TAdvert>.GetMapperConfiguration());
        }

        /// <summary>
        /// Get all advertising
        /// </summary>
        /// <returns>List of advertising DTOs</returns>
        public async Task<IEnumerable<TAdvertDTO>> GetAll()
        {
            List<TAdvertDTO> advertsDTO = new List<TAdvertDTO>();
            var adverts = await _advertRepository.GetAll();

            advertsDTO = _mapper.Map<IEnumerable<TAdvert>, List<TAdvertDTO>>(adverts);
            return advertsDTO;
        }

        /// <summary>
        /// Get advertising by Id
        /// </summary>
        /// <param name="advertId"></param>
        /// <returns>Advertising DTO</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TAdvertDTO> GetById(int? advertId)
        {
            int? id = advertId;
            if (id == null)
                throw new ArgumentNullException();

            var advert = await _advertRepository.GetById(id);

            if (advert == null)
                throw new ArgumentNullException();

            var advertDTO = _mapper.Map<TAdvert, TAdvertDTO>(advert);
            return advertDTO;
        }

        /// <summary>
        /// Delete advertising
        /// </summary>
        /// <param name="advertId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Delete(int? advertId)
        {
            int? id = advertId;
            if (id == null)
                throw new ArgumentNullException();

            await _advertRepository.Delete(id);
        }
    }
}
