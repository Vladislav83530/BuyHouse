using AutoMapper;
using BuyHouse.BLL.Services.Providers.DateTimeProvider;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.UnitTests.BuyHouse.DAL;
using BuyHouse.WEB.Controllers;
using BuyHouse.WEB.Models.AdvertModel;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FlatAdvertController = BuyHouse.API.Controllers.FlatAdvertController;

namespace BuyHouse.UnitTests.BuyHouse.API.Controllers
{
    [TestFixture]
    public class FlatAdvertControllerTests
    {
        public ApplicationDbContext _dbContext;
        public IDateTimeProvider _dateTimeProvider;
        public IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            _dbContext = ApplicationDbContextMocker.GetApplicationDbContext(nameof(FlatAdvertControllerTests));
            _dateTimeProvider = A.Fake<IDateTimeProvider>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapper_API());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }

        #region GetFlatAdvertById
        [TestCase(1, 200)]
        [TestCase(1000, 404)]
        public async Task GetFlatAdvertById_VariousId_ReturnsOkOrNotFound(int id, int expected)
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, null, null);

            //Act
            var result = await controller.GetFlatAdvertById(id) as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(expected));
            if (result.StatusCode == 200)
                Assert.IsInstanceOf<FlatAdvert>(result.Value);
        }

        [Test]
        public async Task GetFlatAdvertById_UnhandledException_ReturnsStatus500()
        {
            //Arrange
            var controller = new FlatAdvertController(null, null, null);
            int id = 1;

            //Act
            var result = await controller.GetFlatAdvertById(id) as IStatusCodeActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(500));
        }
        #endregion

        #region CreateFlatAdvert
        [Test]
        public async Task CreateFlatAdvert_CorrectRequestData_ReturnsOk()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, _dateTimeProvider);
            var advert = RandomGeneratorData.GenerateCorrectData();
            var currentUserId = Guid.NewGuid().ToString();

            //Act
            var result = await controller.CreateFlatAdvert(advert, currentUserId) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<FlatAdvert>(result.Value);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }


        [Test]
        public async Task CreateFlatAdvert_NullData_ReturnsBadRequest()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, null, _dateTimeProvider);
            var currentUserId = Guid.NewGuid().ToString();

            //Act
            var result = await controller.CreateFlatAdvert(null, currentUserId) as IStatusCodeActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task CreateFlatAdvert_NullUserId_ReturnsNotFound()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, null, _dateTimeProvider);
            var advert = RandomGeneratorData.GenerateCorrectData();

            //Act
            var result = await controller.CreateFlatAdvert(advert, null) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task CreateFlatAdvert_UnhandledException_ReturnsStatus500()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, null);
            var advert = RandomGeneratorData.GenerateUncorrectData();
            var currentUserId = Guid.NewGuid().ToString();

            //Act
            var result = await controller.CreateFlatAdvert(advert, currentUserId) as IStatusCodeActionResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(500));
        }
        #endregion

        #region UpdateFlarAdvert
        [TestCase(5, "7e2d1201-b812-40c9-abea-39c1e89154be", 400)]
        [TestCase(1, "7e2d1201-b812-40c9-abea-39c1e89154be", 200)]
        [TestCase(1, "TestString", 400)]
        public async Task UpdateFlatAdvert_CorrectUpdateDataAndVariousAdvertIdOrUserId_ReturnstOkOrBadRequest(int id, string currentUserId, int expected)
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, _dateTimeProvider);
            var advert = RandomGeneratorData.GenerateForUpdatetData();
            advert.Id = 1;

            //Act
            var result = await controller.UpdateFlatAdvert(id, advert, currentUserId) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(expected));
            if(result.StatusCode == 200)
                Assert.IsInstanceOf<FlatAdvert>(result.Value);
        }

        [Test]
        public async Task UpdateFlatAdvert_UncorrectUpdatedAdvertId_ReturnstNotFound()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, _dateTimeProvider);
            var advert = RandomGeneratorData.GenerateForUpdatetData();
            var currentUserId = "7e2d1201-b812-40c9-abea-39c1e89154be";
            advert.Id = 5;
            var id = 5;

            //Act
            var result = await controller.UpdateFlatAdvert(id, advert, currentUserId) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task UpdateFlatAdvert_UnhandledException_ReturnsStatus500()
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, _dateTimeProvider);
            FlatAdvertModel advert = null;
            var currentUserId = "7e2d1201-b812-40c9-abea-39c1e89154be";
            var id = 5;

            //Act
            var result = await controller.UpdateFlatAdvert(id, advert, currentUserId) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(500));
        }
        #endregion

        #region DeleteFlatAdvert
        [TestCase(1, "7e2d1201-b812-40c9-abea-39c1e89154be", 200)]
        [TestCase(1, "", 404)]
        [TestCase(1, "TestString", 400)]
        [TestCase(999, "7e2d1201-b812-40c9-abea-39c1e89154be", 500)]
        public async Task DeleteFlatAdvert_VariousAdvertIdOrUserId_ReturnstObjectResult(int id, string currentUserId, int expected)
        {
            //Arrange
            var controller = new FlatAdvertController(_dbContext, _mapper, _dateTimeProvider);

            //Act
            var result = await controller.DeleteFlatAdvert(id, currentUserId) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(expected));
            if (result.StatusCode == 200)
                Assert.IsInstanceOf<FlatAdvert>(result.Value);
        }
        #endregion
    }
}

