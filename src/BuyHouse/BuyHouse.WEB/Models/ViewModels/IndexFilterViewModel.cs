namespace BuyHouse.WEB.Models.ViewModels
{
    public class IndexFilterViewModel<TAdvert, TFilter>
    {
        public IEnumerable<TAdvert> RealtyAdverts { get; set; }
        public TFilter RealtyAdvertFilter { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<int> LikedAdvert { get; set;}
    }
}