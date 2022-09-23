using BuyHouse.BLL.Services.Abstract;
using BuyHouse.BLL.Services.Providers.JwtTokenProvider;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.HttpClientModel;
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

        public BuyHouseAPIClient(IPhotosService photosService, HttpClient client, HttpRequestMessage request, IJwtTokenProvider tokenProvider)
        {
            _client = client;
            _address = "https://localhost:7122";
            _request = request;
            _photosService = photosService;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Get flat advert (client method)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Flat advert</returns>
        public async Task<FlatAdvert> GetFlatAdvertByID(int? Id)
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
        /// <param name="requestModel"></param>
        /// <param name="uploads"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Created flat advert</returns>
        public async Task<FlatAdvert> CreateFlatAdvert(CreateRequestModel requestModel, IFormFileCollection uploads, string? currentUserId)
        {
            var JwtToken = await _tokenProvider.GenerateJwtToken(currentUserId);

            if (currentUserId == null)
                throw new ArgumentNullException("User Id can't be null");

            requestModel.RealtyPhotos = await _photosService.AddPhotoToAdvert(uploads, currentUserId);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            var response = await _client.PostAsJsonAsync(_address + $"/api/FlatAdvert?currentUserId={currentUserId}", requestModel);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            throw new ArgumentNullException("Not found created advert");
        }
    }
}
