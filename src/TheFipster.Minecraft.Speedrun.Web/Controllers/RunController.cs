using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Enhancer;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IQuickestEventEnhancer _quickestEventEnhancer;
        private readonly IPlayerEventEnhancer _playerEventEnhancer;
        private readonly ITimingStore _timingStore;

        public RunController(
            IRunFinder runFinder,
            IQuickestEventEnhancer quickestEventEnhancer,
            IPlayerEventEnhancer playerEventEnhancer,
            ITimingStore timingStore)
        {
            _runFinder = runFinder;
            _quickestEventEnhancer = quickestEventEnhancer;
            _playerEventEnhancer = playerEventEnhancer;
            _timingStore = timingStore;
        }

        public IActionResult Name(string worldName)
        {
            var run = _runFinder.GetByName(worldName);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Enhance(run);
            viewmodel.PlayerEvents = _playerEventEnhancer.Enhance(run);
            viewmodel.Timings = _timingStore.Get(run.Id);

            return View("Index", viewmodel);
        }

        public IActionResult Index(int index)
        {
            var run = _runFinder.GetByIndex(index);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Enhance(run);
            viewmodel.PlayerEvents = _playerEventEnhancer.Enhance(run);
            viewmodel.Timings = _timingStore.Get(run.Id);

            return View(viewmodel);
        }
    }
}