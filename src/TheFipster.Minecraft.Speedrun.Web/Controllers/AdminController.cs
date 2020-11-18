using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfigService _config;
        private readonly IRunStore _runStore;

        public AdminController(IConfigService config, IRunStore runStore)
        {
            _config = config;
            _runStore = runStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReindexRuns()
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
    }
}