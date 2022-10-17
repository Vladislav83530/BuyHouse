using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.HelperEnum;
using System.Collections.ObjectModel;

namespace BuyHouse.UnitTests.BuyHouse.DAL
{
    public static class ApplicationDbContextExtensions
    {
        public static void Seed(this ApplicationDbContext dbContext)
        {
            dbContext.FlatAdverts.Add(new FlatAdvert
            {
                Id = 1,
                MainInfo = new RealtyMainInfo
                {
                    Region = "Kyiv region",
                    City = "Kyiv",
                    Street = "the avenue Instrument",
                    HouseNumber = "10a",
                    FlatNumber = 56,
                    RegistrationDate = Convert.ToDateTime("10/12/2010 00:00:00"),
                },
                Photos = new Collection<RealtyPhoto>()
                {
                    new RealtyPhoto{ Id=1, Name="photo1", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto1.jpg"},
                    new RealtyPhoto{ Id=2, Name="photo2", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto2.jpg"},
                    new RealtyPhoto{ Id=3, Name="photo3", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto3.jpg"},
                },
                Description = "Some description1",
                Type = TypeOfRealty.Secondary,
                Rooms = 3,
                TypeOfWalls = TypeOfWalls.Monolith,
                TotalArea = 300,
                LivingArea = 280,
                Floor = 10,
                TotalCountFloors = 14,
                Heating = TypeOfHeating.Individual,
                YearBuilt = 2002,
                RegistrationNumber = "2547814547852",
                TotalPrice = 100000,
                PricePerSquareMeter = 333,
                Currency = Currency.USD,
                TypePrice = TypeOfPrice.TotalPrice,
                CreationDate = DateTime.UtcNow,
                LikeCount = 24,
                UserID = "7e2d1201-b812-40c9-abea-39c1e89154be"
            });

            dbContext.FlatAdverts.Add(new FlatAdvert
            {
                Id = 2,
                MainInfo = new RealtyMainInfo
                {
                    Region = "Lviv region",
                    City = "Lviv",
                    Street = "Shevchenka street",
                    HouseNumber = "45b",
                    FlatNumber = 105,
                    RegistrationDate = Convert.ToDateTime("10/12/2000 00:00:00"),
                },
                Photos = new Collection<RealtyPhoto>()
                {
                    new RealtyPhoto{ Id=4, Name="photo1", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto4.jpg"},
                    new RealtyPhoto{ Id=5, Name="photo2", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto2.jpg"},
                    new RealtyPhoto{ Id=6, Name="photo3", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto3.jpg"},
                },
                Description = "Some description2",
                Type = TypeOfRealty.Secondary,
                Rooms = 2,
                TypeOfWalls = TypeOfWalls.FoamBlock,
                TotalArea = 250,
                LivingArea = 130,
                Floor = 9,
                TotalCountFloors = 15,
                Heating = TypeOfHeating.Individual,
                YearBuilt = 1990,
                RegistrationNumber = "1457814547852",
                TotalPrice = 90000,
                PricePerSquareMeter = 360,
                Currency = Currency.Euro,
                TypePrice = TypeOfPrice.TotalPrice,
                CreationDate = DateTime.UtcNow,
                LikeCount = 2,
                UserID = "7e2d1201-b812-40c9-abea-39c1e89154be"
            });

            dbContext.FlatAdverts.Add(new FlatAdvert
            {
                Id = 3,
                MainInfo = new RealtyMainInfo
                {
                    Region = "Kyiv region",
                    City = "Kyiv",
                    Street = "Some street",
                    HouseNumber = "785a",
                    FlatNumber = 81,
                    RegistrationDate = Convert.ToDateTime("10/12/2011 00:00:00"),
                },
                Photos = new Collection<RealtyPhoto>()
                {
                    new RealtyPhoto{ Id=10, Name="photo1", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto1.jpg"},
                    new RealtyPhoto{ Id=11, Name="photo2", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto4.jpg"},
                    new RealtyPhoto{ Id=12, Name="photo3", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto5.jpg"},
                },
                Description = "Some description1",
                Type = TypeOfRealty.Secondary,
                Rooms = 1,
                TypeOfWalls = TypeOfWalls.Monolith,
                TotalArea = 100,
                LivingArea = 80,
                Floor = 14,
                TotalCountFloors = 14,
                Heating = TypeOfHeating.Individual,
                YearBuilt = 2005,
                RegistrationNumber = "2547814547852",
                TotalPrice = 120000,
                PricePerSquareMeter = 120,
                Currency = Currency.USD,
                TypePrice = TypeOfPrice.TotalPrice,
                CreationDate = DateTime.UtcNow,
                LikeCount = 74,
                UserID = "7e2d1201-b812-40c9-abea-39c1e89154be"
            });

            dbContext.FlatAdverts.Add(new FlatAdvert
            {
                Id = 4,
                MainInfo = new RealtyMainInfo
                {
                    Region = "Kherson region",
                    City = "Kherson",
                    Street = "Some street",
                    HouseNumber = "80a",
                    FlatNumber = 80,
                    RegistrationDate = Convert.ToDateTime("10/12/2011 00:00:00"),
                },
                Photos = new Collection<RealtyPhoto>()
                {
                    new RealtyPhoto{ Id=7, Name="photo1", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto1.jpg"},
                    new RealtyPhoto{ Id=8, Name="photo2", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto4.jpg"},
                    new RealtyPhoto{ Id=9, Name="photo3", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto5.jpg"},
                },
                Description = "Some description1",
                Type = TypeOfRealty.Secondary,
                Rooms = 1,
                TypeOfWalls = TypeOfWalls.Monolith,
                TotalArea = 100,
                LivingArea = 80,
                Floor = 12,
                TotalCountFloors = 14,
                Heating = TypeOfHeating.Individual,
                YearBuilt = 2005,
                RegistrationNumber = "2547814547852",
                TotalPrice = 40000,
                PricePerSquareMeter = 40,
                Currency = Currency.USD,
                TypePrice = TypeOfPrice.TotalPrice,
                CreationDate = DateTime.UtcNow,
                LikeCount = 14,
                UserID = "7e2d1201-b812-40c9-abea-39c1e89154be"
            });

            dbContext.FlatAdverts.Add(new FlatAdvert
            {
                Id = 5,
                MainInfo = new RealtyMainInfo
                {
                    Region = "Lviv region",
                    City = "Lviv",
                    Street = "Some street",
                    HouseNumber = "74c",
                    FlatNumber = 80,
                    RegistrationDate = Convert.ToDateTime("10/12/2014 00:00:00"),
                },
                Photos = new Collection<RealtyPhoto>()
                {
                    new RealtyPhoto{ Id=13, Name="photo1", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto1.jpg"},
                    new RealtyPhoto{ Id=14, Name="photo2", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto4.jpg"},
                    new RealtyPhoto{ Id=15, Name="photo3", Path="/Files/7e2d1201-b812-40c9-abea-39c1e89154bephoto5.jpg"},
                },
                Description = "Some description1",
                Type = TypeOfRealty.Secondary,
                Rooms = 1,
                TypeOfWalls = TypeOfWalls.Brick,
                TotalArea = 200,
                LivingArea = 160,
                Floor = 2,
                TotalCountFloors = 5,
                Heating = TypeOfHeating.Individual,
                YearBuilt = 2005,
                RegistrationNumber = "2547814547852",
                TotalPrice = 30000,
                PricePerSquareMeter = 30,
                Currency = Currency.Euro,
                TypePrice = TypeOfPrice.TotalPrice,
                CreationDate = DateTime.UtcNow,
                LikeCount = 54,
                UserID = "7e2d1201-b812-40c9-abea-39c1e89154be"
            });

            dbContext.SaveChanges();
        }
    }
}
