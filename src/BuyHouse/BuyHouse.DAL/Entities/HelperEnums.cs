
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
        UAH,
        Any
    }

    public enum TypeOfWalls
    {
        Brick,
        Frame,
        Panel
    }

    public enum TypeOfPrice
    {
        TotalPrice,
        PerSquareMeter
    }

    public enum TypeOfRealty
    {
        Primary,
        Secondary
    }
}
