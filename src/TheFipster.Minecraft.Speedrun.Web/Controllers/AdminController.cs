using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;

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
        private readonly IMapRenderModule _mapRenderer;
        private readonly IRenderQueue _renderQueue;

        public AdminController(
            IImportReader importReader,
            IAnalyticsWriter analyticsWriter,
            IAnalyticsReader analyticsReader,
            IRunIndexer runIndexer,
            IAnalyticsModule analyticsModules,
            IWorldFinder worldFinder,
            IWorldArchivist worldArchivist,
            IWorldLoader worldLoader,
            IWorldDeleter worldDeleter,
            IMapRenderModule mapRenderer,
            IRenderQueue renderQueue)
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
            _mapRenderer = mapRenderer;
            _renderQueue = renderQueue;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewmodel = new AdminIndexViewModel();
            return View(viewmodel);
        }

        [HttpGet("runs")]
        public IActionResult Runs()
        {
            var viewmodel = new AdminListViewModel();

            var runs = _analyticsReader
                .Get()
                .Select(x => new RunAdminViewModel(x));

            var jobs = _mapRenderer.GetJobs();
            var results = _mapRenderer.GetResults();

            foreach (var run in runs)
            {
                var job = jobs.FirstOrDefault(x => x.Worldname == run.Worldname);
                if (job != null)
                    run.HasPendingRenderJob = true;

                var result = results.FirstOrDefault(x => x.Worldname == run.Worldname);
                if (result != null)
                    run.HasRenderedMap = true;

                viewmodel.Runs.Add(run);
            }

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

        [HttpGet("rendermap/{worldname}")]
        public IActionResult RenderMap(string worldname)
        {
            _mapRenderer.CreateJob(worldname);
            return RedirectToAction("Runs");
        }

        [HttpGet("jobs")]
        public IActionResult RenderJobs()
        {
            var viewmodel = new AdminRenderJobsViewModel();

            viewmodel.Results = _mapRenderer.GetResults();
            viewmodel.Jobs = _renderQueue.PeakAll();
            viewmodel.Active = _renderQueue.Active;

            return View(viewmodel);
        }
    }
}