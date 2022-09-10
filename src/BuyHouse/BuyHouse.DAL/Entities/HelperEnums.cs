
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.DAL.Entities.HelperEnum
{
    public enum Currency
    {
        [Display(Name = "$")]
        USD,
        [Display(Name = "€")]
        Euro,
        [Display(Name = "₴")]
        UAH
    }

    public enum TypeOfWalls
    {
        [Display(Name = "Цегла")]
        Brick,
        [Display(Name = "Каркасний")]
        Frame,
        [Display(Name = "Панель")]
        Panel
    }
}
