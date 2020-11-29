using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfigService _config;
        private readonly IRunImportStore _importStore;
        private readonly IRunAnalyticsStore _analyticsStore;
        private readonly IAnalyticsModule _analyticsModules;

        public AdminController(IConfigService config, IRunImportStore importStore, IRunAnalyticsStore analyticsStore, IAnalyticsModule analyticsModules)
        {
            _config = config;
            _importStore = importStore;
            _analyticsStore = analyticsStore;
            _analyticsModules = analyticsModules;
        }

        public IActionResult Index()
            => View();

        public IActionResult ReIndex()
        {
            _analyticsStore.Index();
            return RedirectToAction("Index");
        }

        public IActionResult ReAnalyze()
        {
            var imports = _importStore.Get();
            foreach (var import in imports)
            {
                var analytics = _analyticsModules.Analyze(import);
                _analyticsStore.Upsert(analytics);
            }

            _analyticsStore.Index();

            return RedirectToAction("Index");
        }
    }
}