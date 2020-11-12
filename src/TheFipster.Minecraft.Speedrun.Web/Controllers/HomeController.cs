using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorldFinder _worldFinder;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;

        public HomeController(ILogger<HomeController> logger, IWorldFinder worldFinder, ILogFinder logFinder, ILogParser logParser)
        {
            _logger = logger;
            _worldFinder = worldFinder;
            _logFinder = logFinder;
            _logParser = logParser;
        }

        public IActionResult Index()
        {
            var worlds = _worldFinder.Find();
            var viewmodel = new HomeIndexViewModel
            {
                Worlds = worlds
            };

            foreach (var world in worlds)
            {
                var logs = _logFinder.Find(world.CreatedOn);
                var parsedLogs = _logParser.Read(logs, world.CreatedOn);
            }

            return View(viewmodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
