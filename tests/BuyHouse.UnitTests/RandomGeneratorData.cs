using Bogus;
using BuyHouse.DAL.Entities.HelperEnum;
using BuyHouse.WEB.Models;
using BuyHouse.WEB.Models.AdvertModel;

namespace BuyHouse.UnitTests
{
    public static class RandomGeneratorData
    {
        public static FlatAdvertModel GenerateCorrectData()
        {
            var advertMainInfo = new Faker<RealtyMainInfoModel>()
                .RuleFor(c => c.Region, f => f.Address.State())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Street, f => f.Address.StreetAddress())
                .RuleFor(c => c.HouseNumber, f => f.Address.BuildingNumber())
                .RuleFor(c => c.FlatNumber, f => f.Random.UInt())
                .RuleFor(c => c.RegistrationDate, f => f.Date.Recent(1970).Date);

            var photo = new Faker<RealtyPhotoModel>()
                .RuleFor(c => c.Id, f => f.Random.Number(10, 100))
                .RuleFor(c => c.Path, f => f.Internet.UrlWithPath());

            var advert = new Faker<FlatAdvertModel>()
                .RuleFor(c => c.Id, f => f.Random.Number(16, 100))
                .RuleFor(c => c.MainInfo, f => advertMainInfo.Generate())
                .RuleFor(c => c.TotalPrice, f => f.Random.ULong())
                .RuleFor(c => c.Photos, f => photo.Generate(4).ToList())
                .RuleFor(c => c.Description, f => f.Random.String(400))
                .RuleFor(c => c.Type, f => f.PickRandom<TypeOfRealty>())
                .RuleFor(c => c.Rooms, f => f.Random.UInt())
                .RuleFor(c => c.TypeOfWalls, f => f.PickRandom<TypeOfWalls>())
                .RuleFor(c => c.TotalArea, f => f.Random.Double(0, double.MaxValue))
                .RuleFor(c => c.LivingArea, f => f.Random.Double(0, double.MinValue))
                .RuleFor(c => c.Floor, f => f.Random.UInt())
                .RuleFor(c => c.TotalCountFloors, f => f.Random.UInt())
                .RuleFor(c => c.Heating, f => f.PickRandom<TypeOfHeating>())
                .RuleFor(c => c.YearBuilt, f => f.Random.UInt(1970, 2026))
                .RuleFor(c => c.RegistrationNumber, f => "47623423852")
                .RuleFor(c => c.TotalPrice, f => f.Random.ULong())
                .RuleFor(c => c.Currency, f => f.PickRandomWithout(Currency.Any))
                .RuleFor(c => c.TypePrice, f => f.PickRandom<TypeOfPrice>())
                .RuleFor(c => c.CreationDate, f => f.Date.Recent(2022))
                .RuleFor(c => c.LikeCount, f => f.Random.UInt())
                .Generate();

            return advert;
        }

        public static FlatAdvertModel GenerateForUpdatetData()
        {
            var advertMainInfo = new Faker<RealtyMainInfoModel>()
                .RuleFor(c => c.Region, f => f.Random.String())
                .RuleFor(c => c.City, f => f.Random.String())
                .RuleFor(c => c.Street, f => f.Random.String())
                .RuleFor(c => c.HouseNumber, f => f.Random.String())
                .RuleFor(c => c.FlatNumber, f => f.Random.UInt())
                .RuleFor(c => c.RegistrationDate, f => f.Date.Recent(1970));

            var advert = new Faker<FlatAdvertModel>()
                .RuleFor(c => c.Id, f => f.Random.Number())
                .RuleFor(c => c.MainInfo, f => advertMainInfo.Generate())
                .RuleFor(c => c.TotalPrice, f => f.Random.ULong())
                .RuleFor(c => c.Description, f => f.Random.String())
                .RuleFor(c => c.Type, f => f.PickRandom<TypeOfRealty>())
                .RuleFor(c => c.Rooms, f => f.Random.UInt())
                .RuleFor(c => c.TypeOfWalls, f => f.PickRandom<TypeOfWalls>())
                .RuleFor(c => c.TotalArea, f => f.Random.Double())
                .RuleFor(c => c.LivingArea, f => f.Random.Double())
                .RuleFor(c => c.Floor, f => f.Random.UInt())
                .RuleFor(c => c.TotalCountFloors, f => f.Random.UInt())
                .RuleFor(c => c.Heating, f => f.PickRandom<TypeOfHeating>())
                .RuleFor(c => c.YearBuilt, f => f.Random.UInt())
                .RuleFor(c => c.RegistrationNumber, f => "234542385232")
                .RuleFor(c => c.TotalPrice, f => f.Random.ULong())
                .RuleFor(c => c.Currency, f => f.PickRandomWithout(Currency.Any))
                .RuleFor(c => c.TypePrice, f => f.PickRandom<TypeOfPrice>())
                .RuleFor(c => c.CreationDate, f => f.Date.Recent())
                .RuleFor(c => c.LikeCount, f => f.Random.UInt())
                .Generate();

            return advert;
        }
    }
}
