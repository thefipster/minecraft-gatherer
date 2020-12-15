using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            _logger.LogDebug("GET Account Login with returnUrl = " + returnUrl);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            _logger.LogDebug("POST Account Login with user = " + login.Email);

            var principal = await _authService.Validate(login);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            if (string.IsNullOrWhiteSpace(login.ReturnUrl))
            {
                _logger.LogDebug("Redirecting to Home (/)");
                return RedirectToAction("Index", "Home");
            }

            _logger.LogTrace($"Redirecting to Return URL ({login.ReturnUrl})");
            return Redirect(login.ReturnUrl);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogDebug("GET Account Logout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}