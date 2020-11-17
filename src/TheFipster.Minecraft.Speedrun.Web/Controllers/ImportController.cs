using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Modules;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportModule _importModule;

        public ImportController(
            IImportModule importModule)
        {
            _importModule = importModule;
        }

        public IActionResult Index()
        {
            var runs = _importModule.Import();
            var viewmodel = new WorldIndexViewModel
            {
                Runs = runs
            };

            return View(viewmodel);
        }

        public IActionResult Force()
        {
            var runs = _importModule.Import(true);
            var viewmodel = new WorldIndexViewModel
            {
                Runs = runs
            };

            return View("Index", viewmodel);
        }
    }
}