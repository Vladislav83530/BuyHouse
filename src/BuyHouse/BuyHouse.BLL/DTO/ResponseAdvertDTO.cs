namespace BuyHouse.BLL.DTO
{
    public class ResponseAdvertDTO<T>
    {
        public IEnumerable<T>? Adverts {get ;set;}
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}