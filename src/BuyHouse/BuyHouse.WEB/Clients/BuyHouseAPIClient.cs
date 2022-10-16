using AutoMapper;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.BLL.Services.Providers.JwtTokenProvider;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BuyHouse.WEB.Clients
{
    public class BuyHouseAPIClient
    {
        private readonly HttpClient _client;
        private static string _address;
        private readonly HttpRequestMessage _request;
        private readonly IPhotosService _photosService;
        private readonly IJwtTokenProvider _tokenProvider;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public BuyHouseAPIClient(IPhotosService photosService, IJwtTokenProvider tokenProvider, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _client = new HttpClient();
            _address = _config.GetValue<string>("APIAdress");
            _request = new HttpRequestMessage();
            _photosService = photosService;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Get flat advert (client method)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Flat advert</returns>
        public async Task<FlatAdvert> GetFlatAdvertByIDAsync(int? Id)
        {
            _request.RequestUri = new Uri(_address + $"/api/FlatAdvert/{Id}");
            var response = await _client.SendAsync(_request);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }

        /// <summary>
        /// Create flat advert (client method)
        /// </summary>
        /// <param name="flatAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Created flat advert</returns>
        public async Task<FlatAdvert> CreateFlatAdvertAsync(FlatAdvertModel flatAdvert, IFormFileCollection uploads, string? currentUserId)
        {

            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            flatAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PostAsJsonAsync(_address + $"/api/FlatAdvert?currentUserId={currentUserId}", flatAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found created advert");
        }

        /// <summary>
        /// Update flat advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <param name="flatAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>updated advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<FlatAdvert> UpdateFlatAdvertAsync(int flatAdvertId, FlatAdvertModel flatAdvert, IFormFileCollection uploads, string? currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            flatAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PutAsJsonAsync(_address + $"/api/FlatAdvert/{flatAdvertId}?currentUserId={currentUserId}", flatAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found edited advert");
        }

        /// <summary>
        /// Delete flat advert
        /// </summary>
        /// <param name="flatAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>deleted flat advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<FlatAdvert> DeleteFlatAdvertAsync(int flatAdvertId, string currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            _request.RequestUri = new Uri(_address + $"/api/FlatAdvert/{flatAdvertId}?currentUserId={currentUserId}");

            var response = await _client.DeleteAsync(_request.RequestUri);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }
    }
}
