using BuyHouse.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.UnitTests.BuyHouse.DAL
{
    public static class ApplicationDbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Seed();

            return dbContext;
        }
    }
}
