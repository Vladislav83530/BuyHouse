using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.WEB.Models.HttpClientModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BuyHouse.WEB.Clients
{
    public class BuyHouseAPIClient
    {
        private readonly HttpClient _client;
        private static string _address;
        private readonly HttpRequestMessage _request;
        private readonly IPhotosService _photosService;

        public BuyHouseAPIClient(IPhotosService photosService, HttpClient client, HttpRequestMessage request)
        {
            _client = client;
            _address = "https://localhost:7122";
            _request = request;
            _photosService = photosService;
        }

        /// <summary>
        /// Get flat advert (client method)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Flat advert</returns>
        public async Task<FlatAdvert> GetFlatAdvertByID(int? Id)
        {
            _request.RequestUri = new Uri(_address + $"/api/FlatAdvert/GetFlatAdvertById/{Id}");
            var response = await _client.SendAsync(_request);

            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            return null;
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
            requestModel.RealtyPhotos = await _photosService.AddPhotoToAdvert(uploads, currentUserId);
            var response =await  _client.PostAsJsonAsync(_address + $"/api/FlatAdvert/CreateFlatAdvert?currentUserId={currentUserId}", requestModel);
            var item = JsonConvert.SerializeObject(requestModel);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var result = JsonConvert.DeserializeObject<FlatAdvert>(content);
                return result;
            }
            return null;
        }
    }
}
