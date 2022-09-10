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
    public class FlatAdvertService : GenericAdvertService<FlatAdvertDTO, FlatAdvert>, IAdvertService<FlatAdvertDTO, FlatAdvert>
    {
        private readonly IRepository<FlatAdvert> _flatAdvertRepository;
        private readonly IPhotosService _photosService;
        private readonly IMapper _mapper;
        public FlatAdvertService(IRepository<FlatAdvert> flatAdvertRepository, IPhotosService photosService) : base(flatAdvertRepository)
        {
            _flatAdvertRepository = flatAdvertRepository;
            _mapper = new Mapper(AutoMapper_BLL<FlatAdvertDTO, FlatAdvert>.GetMapperConfiguration());
            _photosService = photosService;
        }

        /// <summary>
        /// Create flat advertising
        /// </summary>
        /// <param name="flatAdvert"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Create(FlatAdvertDTO flatAdvertDTO, IFormFileCollection uploads, string? currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException(nameof(currentUserId));

            FlatAdvert flatAdvert = new FlatAdvert();
            List<RealtyPhotoDTO> photos = new List<RealtyPhotoDTO>();
            photos = await _photosService.AddPhoto(uploads, currentUserId);

            flatAdvert = _mapper.Map<FlatAdvertDTO, FlatAdvert>(flatAdvertDTO);
            flatAdvert.UserID = currentUserId;
            flatAdvert.CreationDate = DateTime.Now;
            flatAdvert.Photos = _mapper.Map<IEnumerable<RealtyPhotoDTO>, List<RealtyPhoto>>(photos);

            await _flatAdvertRepository.Create(flatAdvert);
        }
    }
}