using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [RegularExpression(@"^(?:\+38)?(0[5-9][0-9]\d{7})$", ErrorMessage = "IncorrectPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "IncorrectAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Compare("Password", ErrorMessage = "PasswordError")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        //Registration by external provider
        public IEnumerable<AuthenticationScheme>? ExternalProviders { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
