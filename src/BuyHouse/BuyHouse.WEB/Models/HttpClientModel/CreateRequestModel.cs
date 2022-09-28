using BuyHouse.DAL.Entities;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.WEB.Models.HttpClientModel
{
    public class CreateRequestModel
    {
        public FlatAdvertModel? FlatAdvert { get; set; }
        public IEnumerable<RealtyPhoto>? RealtyPhotos { get; set; }
    }
}
