using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("operations")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IImportReader _importReader;
        private readonly IAnalyticsReader _analyticsReader;
        private readonly IAnalyticsWriter _analyticsWriter;

        public IRunIndexer _runIndexer { get; }

        private readonly IAnalyticsModule _analyticsModules;
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldArchivist _worldArchivist;
        private readonly IWorldLoader _worldLoader;
        private readonly IWorldDeleter _worldDeleter;

        public AdminController(
            IImportReader importReader,
            IAnalyticsWriter analyticsWriter,
            IAnalyticsReader analyticsReader,
            IRunIndexer runIndexer,
            IAnalyticsModule analyticsModules,
            IWorldFinder worldFinder,
            IWorldArchivist worldArchivist,
            IWorldLoader worldLoader,
            IWorldDeleter worldDeleter)
        {
            _importReader = importReader;
            _analyticsReader = analyticsReader;
            _analyticsWriter = analyticsWriter;
            _runIndexer = runIndexer;
            _analyticsModules = analyticsModules;
            _worldFinder = worldFinder;
            _worldArchivist = worldArchivist;
            _worldLoader = worldLoader;
            _worldDeleter = worldDeleter;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var analytics = _analyticsReader.Get();
            var viewmodel = new AdminIndexViewModel();
            viewmodel.Runs = analytics;
            return View(viewmodel);
        }

        [HttpGet("reindex")]
        public IActionResult ReIndex()
        {
            _runIndexer.Index();
            return RedirectToAction("Index");
        }

        [HttpGet("reanalyze")]
        public IActionResult ReAnalyze()
        {
            var imports = _importReader.Get();
            foreach (var import in imports)
            {
                var analytics = _analyticsModules.Analyze(import);
                _analyticsWriter.Upsert(analytics);
            }

            _runIndexer.Index();

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