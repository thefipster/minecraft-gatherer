using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServerPropertiesReader _serverPropertiesReader;
        private readonly IRunFinder _runFinder;

        public HomeController(IServerPropertiesReader serverPropertiesReader, IRunFinder runFinder)
        {
            _serverPropertiesReader = serverPropertiesReader;
            _runFinder = runFinder;
        }

        public IActionResult Index()
        {
            var viewmodel = new HomeIndexViewModel();
            viewmodel.ServerProperties = _serverPropertiesReader.Read();
            viewmodel.LatestRuns = _runFinder.GetStarted().OrderByDescending(x => x.World.CreatedOn).Take(5);
            return View(viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
