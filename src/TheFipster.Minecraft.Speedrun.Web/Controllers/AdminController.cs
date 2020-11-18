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
            var counter = 1;
            foreach (var run in allRuns.OrderBy(x => x.World.CreatedOn))
            {
                if (run.Validity.IsValid)
                {
                    run.Index = counter;
                    counter++;
                }
                else
                {
                    run.Index = 0;
                }

                _runStore.Update(run);
            }

            return RedirectToAction("Index");
        }
    }
}