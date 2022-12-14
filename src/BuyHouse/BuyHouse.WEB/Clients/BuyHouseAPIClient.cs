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

        #region Flat Advert
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
        #endregion

        #region House Advert
        /// <summary>
        /// Get house advert
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>House advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HouseAdvert> GetHouseAdvertByIdAsync(int Id)
        {
            _request.RequestUri = new Uri(_address + $"/api/HouseAdvert/{Id}");
            var response = await _client.SendAsync(_request);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<HouseAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }

        /// <summary>
        /// Create house advert (client method)
        /// </summary>
        /// <param name="houseAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>House advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HouseAdvert> CreateHouseAdvertAsync(HouseAdvertModel houseAdvert, IFormFileCollection uploads, string? currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            houseAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PostAsJsonAsync(_address + $"/api/HouseAdvert?currentUserId={currentUserId}", houseAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<HouseAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found created advert");
        }

        /// <summary>
        /// Update house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="houseAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>updated advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HouseAdvert> UpdateHouseAdvertAsync(int houseAdvertId, HouseAdvertModel houseAdvert, IFormFileCollection uploads, string? currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            houseAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PutAsJsonAsync(_address + $"/api/HouseAdvert/{houseAdvertId}?currentUserId={currentUserId}", houseAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<HouseAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found edited advert");
        }

        /// <summary>
        /// Delete house advert
        /// </summary>
        /// <param name="houseAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>deleted house advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HouseAdvert> DeleteHouseAdvertAsync(int houseAdvertId, string currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            _request.RequestUri = new Uri(_address + $"/api/HouseAdvert/{houseAdvertId}?currentUserId={currentUserId}");

            var response = await _client.DeleteAsync(_request.RequestUri);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<HouseAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }
        #endregion

        #region Room Advert
        /// <summary>
        /// Get room advert (client method)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Room advert</returns>
        public async Task<RoomAdvert> GetRoomAdvertByIdAsync(int? Id)
        {
            _request.RequestUri = new Uri(_address + $"/api/RoomAdvert/{Id}");
            var response = await _client.SendAsync(_request);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<RoomAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }

        /// <summary>
        /// Create room advert (client method)
        /// </summary>
        /// <param name="roomAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Created room advert</returns>
        public async Task<RoomAdvert> CreateRoomAdvertAsync(RoomAdvertModel roomAdvert, IFormFileCollection uploads, string? currentUserId)
        {

            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            roomAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PostAsJsonAsync(_address + $"/api/RoomAdvert?currentUserId={currentUserId}", roomAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<RoomAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found created advert");
        }

        /// <summary>
        /// Update room advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <param name="roomAdvert"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>updated advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<RoomAdvert> UpdateRoomAdvertAsync(int roomAdvertId, RoomAdvertModel roomAdvert, IFormFileCollection uploads, string? currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);

            roomAdvert.Photos = _mapper.Map<ICollection<RealtyPhoto>, ICollection<RealtyPhotoModel>>(await _photosService.AddPhotoToAdvertAsync(uploads, currentUserId));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PutAsJsonAsync(_address + $"/api/RoomAdvert/{roomAdvertId}?currentUserId={currentUserId}", roomAdvert);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<RoomAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found edited advert");
        }

        /// <summary>
        /// Delete room advert
        /// </summary>
        /// <param name="roomAdvertId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>deleted room advert</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<RoomAdvert> DeleteRoomAdvertAsync(int roomAdvertId, string currentUserId)
        {
            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            var JwtToken = await _tokenProvider.ProvideJwtTokenAsync(currentUserId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            _request.RequestUri = new Uri(_address + $"/api/RoomAdvert/{roomAdvertId}?currentUserId={currentUserId}");

            var response = await _client.DeleteAsync(_request.RequestUri);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<RoomAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found content");
        }
        #endregion
    }
}
