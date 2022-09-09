
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

    public enum TypePrice
    {
        [Display(Name = "Price of an object")]
        AllPrice,
        [Display(Name = "Price per square meter")]
        PricePerSquareMeter
    }

    public enum TypeOfRealty
    {
        [Display(Name = "Secondary housing")]
        Secondary,
        [Display(Name = "Primary housing")]
        Primary
    }

    public enum Heating
    {
        Centralized,
        Individual,
        Combined,
        [Display(Name = "Without heating")]
        WithoutHeating
    }
}
