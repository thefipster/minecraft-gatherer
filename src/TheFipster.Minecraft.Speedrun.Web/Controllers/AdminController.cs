using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IImportStore _importStore;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAnalyticsModule _analyticsModules;

        public AdminController(
            IImportStore importStore,
            IAnalyticsStore analyticsStore,
            IAnalyticsModule analyticsModules)
        {
            _importStore = importStore;
            _analyticsStore = analyticsStore;
            _analyticsModules = analyticsModules;
        }

        public IActionResult Index()
        {
            var analytics = _analyticsStore.Get();
            var viewmodel = new AdminIndexViewModel();
            viewmodel.Runs = analytics;
            return View(viewmodel);
        }

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