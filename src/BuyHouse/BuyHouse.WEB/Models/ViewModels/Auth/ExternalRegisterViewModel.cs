using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.ViewModels.Auth
{
    public class ExternalRegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "IncorrectAddress")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+38)?(0[5-9][0-9]\d{7})$", ErrorMessage = "IncorrectPhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
