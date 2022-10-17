using BuyHouse.BLL.Services;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.UnitTests.BuyHouse.DAL;


namespace BuyHouse.UnitTests.BuyHouse.BLL.Services
{
    [TestFixture]
    internal class FlatAdvertFilterServiceTests
    {
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dbContext = ApplicationDbContextMocker.GetApplicationDbContext(nameof(FlatAdvertFilterServiceTests));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetMostLikedFlatAdvertAsync_ReturnsMostLikedAdverts()
        {
            //Arrange
            var service = new FlatAdvertFilterService(_dbContext, null);

            //Act
            var result = await service.GetMostLikedFlatAdvertAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<FlatAdvert>>(result);
            Assert.That(result.Count, Is.EqualTo(3));
            foreach(var advert in result)
                Assert.That(advert.LikeCount, Is.GreaterThanOrEqualTo(24));
        }

        [TestCase("7e2d1201-b812-40c9-abea-39c1e89154be")]
        [TestCase("")]
        public async Task GetSellersFlatAdvertsAsync_VariousUserId_ReturnsExceptionOrAdverts(string currentUserId)
        {
            var service = new FlatAdvertFilterService(_dbContext, null);
            string expectedErrorMessage = "Current user Id can not be null or empty";

            if (!String.IsNullOrEmpty(currentUserId))
            {
                var result = await service.GetSellersFlatAdvertsAsync(currentUserId);

                Assert.NotNull(result);
                Assert.IsInstanceOf<IEnumerable<FlatAdvert>>(result);
                foreach (var advert in result)
                    Assert.That(advert.UserID, Is.EqualTo(currentUserId));
            }
            if (String.IsNullOrEmpty(currentUserId))
            {
                var ex = Assert.ThrowsAsync<Exception>(() => service.GetSellersFlatAdvertsAsync(currentUserId));
                Assert.AreSame(ex.Message, expectedErrorMessage);
            }
        }
    }
}
