using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Modules;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfigService _config;
        private readonly IRunStore _runStore;
        private readonly IAnalyticsModule _analyticsModules;
        private readonly ITimingStore _timingStore;

        public AdminController(IConfigService config, IRunStore runStore, IAnalyticsModule analyticsModules, ITimingStore timingStore)
        {
            _config = config;
            _runStore = runStore;
            _analyticsModules = analyticsModules;
            _timingStore = timingStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReIndex()
        {
            var allRuns = _runStore.Get();
            var counter = 1 + _config.InitialRunIndex;
            foreach (var run in allRuns.OrderBy(x => x.World.CreatedOn))
            {
                run.Index = counter;
                counter++;
                _runStore.Update(run);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReAnalyze()
        {
            var runs = _runStore.Get();
            foreach (var run in runs)
            {
                var analysis = _analyticsModules.Analyze(run);
                if (_timingStore.Exists(run.World.Name))
                    _timingStore.Update(analysis.Timings);
                else
                    _timingStore.Add(analysis.Timings);
            }

            return RedirectToAction("Index");
        }
    }
}