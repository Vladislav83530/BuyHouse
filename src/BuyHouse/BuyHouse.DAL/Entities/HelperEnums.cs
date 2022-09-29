
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
        Panel,
        FoamBlock,
        Ferroconcrete,
        AeratedСoncrete,
        BlockBrick,
        Monolith
    }

    public enum TypeOfPrice
    {
        TotalPrice,
        PerSquareMeter
    }

    public enum TypeOfRealty
    {
        [Display(Name ="Some primary name")]
        Primary,
        Secondary
    }

    public enum TypeOfHeating
    {
        Individual,
        Centralized,
        Сombined,
        WithoutHeating
    }
}
