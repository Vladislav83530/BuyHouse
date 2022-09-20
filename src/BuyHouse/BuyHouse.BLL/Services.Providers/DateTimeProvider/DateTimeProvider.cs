namespace BuyHouse.BLL.Services.Providers.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()=> DateTime.UtcNow;
    }
}
