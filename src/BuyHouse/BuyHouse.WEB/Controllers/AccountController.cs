using BuyHouse.DAL.Entities.ApplicationUserEntities;
using BuyHouse.WEB.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BuyHouse.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<AccountController> _localizer;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<AccountController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        /// <summary>
        /// Log in (View)
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>log in page</returns>
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var externalProvider = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel { ReturnUrl = returnUrl, ExternalProviders = externalProvider });
        }

        /// <summary>
        /// Log in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirect to Index view if login success</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                model.ExternalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", _localizer["Incorrect login or password"]);
            }
            return View(model);
        }

        /// <summary>
        /// Register (view)
        /// </summary>
        /// <returns>register view</returns>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var externalProvider = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new RegisterViewModel {ExternalProviders = externalProvider});
        }

        //TODO: same email (error)
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>rediret to Index action if registration success</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var externalProvider = await _signInManager.GetExternalAuthenticationSchemesAsync();
                model.ExternalProviders = externalProvider;
                ApplicationUser user = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Surname = model.Surname,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Log out
        /// </summary>
        /// <returns>Index (Home) view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Login by external provider view
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// Login by external provider 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                return RedirectToAction("Login");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            string? userName = info.Principal.FindFirst(ClaimTypes.Name)?.Value.Split(' ')[0];
            string? userSurname = info.Principal.FindFirst(ClaimTypes.Surname)?.Value;
            var userEmail = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var userPhoneNumber = info.Principal.FindFirst(ClaimTypes.MobilePhone)?.Value;
            return RedirectToAction("ExternalRegister", new ExternalRegisterViewModel
            {
                Name = userName,
                Surname = userSurname,
                Email = userEmail,
                PhoneNumber = userPhoneNumber
            });
        }


        //TODO: same email (error)
        /// <summary>
        /// Register by external provider
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                return RedirectToAction("Login");

            ApplicationUser user = new()
            {
                Email = vm.Email,
                UserName = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname
            };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return View(vm);

            result = await _userManager.AddLoginAsync(user, info);

            if (!result.Succeeded)
                return View(vm);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
    }
}
