using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IQuickestEventEnhancer _quickestEventEnhancer;
        private readonly IPlayerEventEnhancer _playerEventEnhancer;

        public RunController(
            IRunFinder runFinder,
            IQuickestEventEnhancer quickestEventEnhancer,
            IPlayerEventEnhancer playerEventEnhancer)
        {
            _runFinder = runFinder;
            _quickestEventEnhancer = quickestEventEnhancer;
            _playerEventEnhancer = playerEventEnhancer;
        }

        public IActionResult Name(string worldname)
        {
            var run = _runFinder.GetByName(worldname);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Enhance(run.Import);
            viewmodel.PlayerEvents = _playerEventEnhancer.Enhance(run.Import);

            return View("Index", viewmodel);
        }

        public IActionResult Index(int index)
        {
            var run = _runFinder.GetByIndex(index);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Enhance(run.Import);
            viewmodel.PlayerEvents = _playerEventEnhancer.Enhance(run.Import);

            return View(viewmodel);
        }
    }
}