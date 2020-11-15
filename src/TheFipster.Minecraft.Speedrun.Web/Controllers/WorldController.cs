using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class WorldController : Controller
    {
        private readonly IWorldFinder _worldFinder;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;

        public WorldController(IWorldFinder worldFinder, ILogFinder logFinder, ILogParser logParser)
        {
            _worldFinder = worldFinder;
            _logFinder = logFinder;
            _logParser = logParser;
        }

        public IActionResult Index()
        {
            var worlds = _worldFinder.Find();
            var viewmodel = new WorldIndexViewModel()
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

        [HttpGet("{timestamp:int}")]
        public IActionResult Detail(int timestamp)
        {
            return View();

        }
    }
}