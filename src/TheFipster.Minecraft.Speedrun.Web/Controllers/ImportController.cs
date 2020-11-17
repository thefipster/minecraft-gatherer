using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheFipster.Minecraft.Speedrun.Modules;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportModule _importModule;
        private readonly ILogger<ImportController> _logger;

        public ImportController(
            IImportModule importModule,
            ILogger<ImportController> logger)
        {
            _importModule = importModule;
            _logger = logger;
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

        [HttpGet("{timestamp:int}")]
        public IActionResult Detail(int timestamp)
        {
            return View();

        }
    }
}