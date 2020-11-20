using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRunFinder _runFinder;

        public HomeController(IRunFinder runFinder)
        {
            _runFinder = runFinder;
        }

        public IActionResult Index()
        {
            var allRuns = _runFinder.GetAll();

            var viewmodel = new HomeIndexViewModel();

            viewmodel.LatestRuns = allRuns.Where(x => x.Validity.IsValid && x.Players.Count() > 1).OrderByDescending(x => x.World.CreatedOn).Take(5);

            viewmodel.RunCounts = new RunCounts();
            viewmodel.RunCounts.All = allRuns.Count();
            viewmodel.RunCounts.Valid = allRuns.Count(x => x.Validity.IsValid);
            viewmodel.RunCounts.Discarded = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() == 1);
            viewmodel.RunCounts.Attempts = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1);
            viewmodel.RunCounts.End = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.State == Outcomes.ResetEnd);
            viewmodel.RunCounts.Finished = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.IsFinished);
            viewmodel.RunCounts.Nether = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.State == Outcomes.ResetNether);
            viewmodel.RunCounts.Search = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.State == Outcomes.ResetSearch);
            viewmodel.RunCounts.Spawn = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.State == Outcomes.ResetSpawn);
            viewmodel.RunCounts.Stronghold = allRuns.Count(x => x.Validity.IsValid && x.Players.Count() > 1 && x.Outcome.State == Outcomes.ResetStronghold);
            viewmodel.RunCounts.Fortresses = allRuns.Count(x => x.Validity.IsValid && x.Events.Any(x => x.Data == "A Terrible Fortress"));

            return View(viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
