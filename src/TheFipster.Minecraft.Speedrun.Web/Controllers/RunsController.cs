using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunsController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly ITimingStore _timingStore;

        public RunsController(IRunFinder runFinder, ITimingStore timingStore)
        {
            _runFinder = runFinder;
            _timingStore = timingStore;
        }

        public IActionResult Index()
        {
            var timings = _timingStore
                .Get()
                .Where(x => x.FinishedOn.HasValue);

            var viewmodel = new RunListViewModel(timings);

            foreach (var timing in timings)
                viewmodel.Runs.Add(_runFinder.GetByName(timing.Worldname));

            return View(viewmodel);
        }

        public IActionResult Started()
        {
            var startedRuns = _runFinder.GetStarted().ToList();
            var viewmodel = new RunListViewModel(startedRuns);
            return View(viewmodel);
        }

        public IActionResult All()
        {
            var validRuns = _runFinder.GetValid().ToList();
            var viewmodel = new RunListViewModel(validRuns);
            return View(viewmodel);
        }

        public IActionResult Garbage()
        {
            var allRuns = _runFinder.GetAll().ToList();
            var viewmodel = new RunListViewModel(allRuns);
            return View(viewmodel);
        }
    }
}