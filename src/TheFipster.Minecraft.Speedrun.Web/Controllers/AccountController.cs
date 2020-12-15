using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRunFinder _runFinder;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, IRunFinder runFinder, ILogger<AccountController> logger)
        {
            _authService = authService;
            _runFinder = runFinder;
            _logger = logger;
        }

        [HttpGet("id/{id}")]
        public IActionResult IndexById(string id)
        {
            var playerId = Guid.Parse(id).ToString();
            var runs = _runFinder.GetAll();

            var viewmodel = new AccountIndexViewModel(playerId);
            viewmodel.Attemps = runs.Count(x => x.Players.Any(y => y.Id == playerId));

            return View("Index", viewmodel);
        }

        [HttpGet("{name}")]
        public IActionResult IndexByName(string name)
        {
            var viewmodel = new AccountIndexViewModel();

            var runs = _runFinder.GetAll();
            viewmodel.Attemps = runs.Count(x => x.Players.Any(y => y.Name == name));

            return View("Index", viewmodel);
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            _logger.LogDebug("GET Account Login with returnUrl = " + returnUrl);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
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