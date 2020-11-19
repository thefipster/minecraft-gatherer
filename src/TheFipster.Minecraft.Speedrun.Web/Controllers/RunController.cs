using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Enhancer;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IFirstToAdvancementEnhancer _advancementEnhancer;

        public RunController(IRunFinder runFinder, IFirstToAdvancementEnhancer advancementEnhancer)
        {
            _runFinder = runFinder;
            _advancementEnhancer = advancementEnhancer;
        }

        public IActionResult Name(string worldName)
        {
            var run = _runFinder.GetByName(worldName);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _advancementEnhancer.Enhance(run);

            return View(viewmodel);
        }

        public IActionResult Index(int index)
        {
            var run = _runFinder.GetByIndex(index);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _advancementEnhancer.Enhance(run);

            return View(viewmodel);
        }
    }
}