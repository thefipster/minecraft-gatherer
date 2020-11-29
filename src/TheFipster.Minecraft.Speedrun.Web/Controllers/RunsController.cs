using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunsController : Controller
    {
        private readonly IRunFinder _runFinder;

        public RunsController(IRunFinder runFinder)
            => _runFinder = runFinder;

        public IActionResult Index()
        {
            var runs = _runFinder.GetFinished();
            var viewmodel = new RunListViewModel(runs);
            return View(viewmodel);
        }

        public IActionResult Started()
        {
            var startedRuns = _runFinder.GetStarted();
            var viewmodel = new RunListViewModel(startedRuns);
            return View(viewmodel);
        }

        public IActionResult All()
        {
            var validRuns = _runFinder.GetValid();
            var viewmodel = new RunListViewModel(validRuns);
            return View(viewmodel);
        }

        public IActionResult Garbage()
        {
            var allRuns = _runFinder.GetAll();
            var viewmodel = new RunListViewModel(allRuns);
            return View(viewmodel);
        }
    }
}