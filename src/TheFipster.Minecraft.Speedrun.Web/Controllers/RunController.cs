using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunController : Controller
    {
        private readonly IRunFinder _runFinder;

        public RunController(IRunFinder runFinder)
        {
            _runFinder = runFinder;
        }

        public IActionResult Index()
        {
            var finishedRuns = _runFinder.GetFinished();
            var viewmodel = new RunListViewModel(finishedRuns);
            return View(viewmodel);
        }

        public IActionResult All()
        {
            var validRuns = _runFinder.GetValid();
            var viewmodel = new RunListViewModel(validRuns);
            return View(viewmodel);
        }

        public IActionResult Detail(string worldName)
        {
            var run = _runFinder.GetByName(worldName);
            var viewmodel = new RunDetailViewModel(run);
            return View(viewmodel);
        }
    }
}