using Microsoft.AspNetCore.Mvc;
using System.IO;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IImportStore _importStore;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IAnalyticsModule _analyticsModules;
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldArchivist _worldArchivist;
        private readonly IWorldLoader _worldLoader;
        private readonly IWorldDeleter _worldDeleter;

        public AdminController(
            IImportStore importStore,
            IAnalyticsStore analyticsStore,
            IAnalyticsModule analyticsModules,
            IWorldFinder worldFinder,
            IWorldArchivist worldArchivist,
            IWorldLoader worldLoader,
            IWorldDeleter worldDeleter)
        {
            _importStore = importStore;
            _analyticsStore = analyticsStore;
            _analyticsModules = analyticsModules;
            _worldFinder = worldFinder;
            _worldArchivist = worldArchivist;
            _worldLoader = worldLoader;
            _worldDeleter = worldDeleter;
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

        [HttpGet("compress/{worldname}")]
        public JsonResult Compress(string worldname)
        {
            var archive = _worldArchivist.Compress(worldname);
            return Json(archive.FullName);
        }

        [HttpGet("decompress/{worldname}")]
        public JsonResult Decompress(string worldname)
        {
            var world = _worldArchivist.Decompress(worldname);
            return Json(world.FullName);
        }

        [HttpGet("find/{worldname}")]
        public JsonResult Find(string worldname)
        {
            var location = _worldFinder.Find(worldname);
            return Json(location.FullName);
        }

        [HttpGet("load/{worldname}")]
        public JsonResult Load(string worldname)
        {
            var worldLocation = _worldFinder.Find(worldname);

            if (!worldLocation.Attributes.HasFlag(FileAttributes.Directory))
                worldLocation = _worldArchivist.Decompress(worldname);

            var worldFolder = new DirectoryInfo(worldLocation.FullName);
            var world = _worldLoader.Load(worldFolder);
            return Json(world);
        }

        [HttpGet("delete/{worldname}")]
        public JsonResult Delete(string worldname)
        {
            _worldDeleter.Delete(worldname);
            return Json(true);
        }
    }
}