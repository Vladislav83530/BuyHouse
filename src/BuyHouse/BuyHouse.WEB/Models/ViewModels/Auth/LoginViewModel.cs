using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace BuyHouse.WEB.Models.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

        public IEnumerable<AuthenticationScheme>? ExternalProviders { get; set; }
    }
}
