using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PennyWise.Interfaces;
using PennyWise.ViewModels;

namespace PennyWise.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly PennyWise.Data.ApplicationDbContext _context;

        public AccountController(IAuthService authService, PennyWise.Data.ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();
            vm.AvailableCurrencies = _context.Currencies
                .Where(c => c.IsActive)
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Code} - {c.Name} ({c.Symbol})"
                })
                .ToList();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
               model.AvailableCurrencies = _context.Currencies
                .Where(c => c.IsActive)
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Code} - {c.Name} ({c.Symbol})"
                })
                .ToList();
                return View(model);
            }

            var user = await _authService.RegisterAsync(model.UserName, model.Email, model.Password, model.CurrencyId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User name already exists.");
                 model.AvailableCurrencies = _context.Currencies
                .Where(c => c.IsActive)
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Code} - {c.Name} ({c.Symbol})"
                })
                .ToList();
                return View(model);
            }

            var principal = _authService.CreateClaimsPrincipal(user);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _authService.ValidateUserAsync(model.UserName, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            var principal = _authService.CreateClaimsPrincipal(user);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

