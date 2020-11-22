using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Enhancer;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IRunCounterEnhancer _runCounter;

        public HomeController(IRunFinder runFinder, IRunCounterEnhancer runCounter)
        {
            _runFinder = runFinder;
            _runCounter = runCounter;
        }

        public IActionResult Index()
        {
            var viewmodel = new HomeIndexViewModel();
            viewmodel.LatestRuns = _runFinder.GetStarted().OrderByDescending(x => x.World.CreatedOn).Take(7);
            viewmodel.RunCounts = _runCounter.Enhance();

            return View(viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
