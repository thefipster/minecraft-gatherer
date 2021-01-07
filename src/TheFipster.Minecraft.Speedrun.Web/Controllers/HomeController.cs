using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Speedrun.Web.Converter;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IRunCounterExtender _runCounter;
        private readonly IRunListConverter _listConverter;
        private readonly IBestTimingsExtender _bestTimings;
        private readonly IPersonalBestExtender _pbs;

        public HomeController(
            IRunFinder runFinder,
            IRunCounterExtender runCounter,
            IRunListConverter listConverter,
            IBestTimingsExtender bestTimings,
            IPersonalBestExtender pbs)
        {
            _runFinder = runFinder;
            _runCounter = runCounter;
            _listConverter = listConverter;
            _bestTimings = bestTimings;
            _pbs = pbs;
        }

        public IActionResult Index()
        {
            var viewmodel = new HomeIndexViewModel();

            var runs = _runFinder
                .GetStarted()
                .OrderByDescending(x => x.Timings.StartedOn)
                .Take(5);

            viewmodel.LatestRuns = _listConverter.Convert(runs);
            viewmodel.RunCounts = _runCounter.Extend();
            viewmodel.FastestSections = _bestTimings.Extend();

            var pbs = _pbs.Extend(5);
            viewmodel.PersonalBests = _listConverter.Convert(pbs);

            return View(viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
    }
}
