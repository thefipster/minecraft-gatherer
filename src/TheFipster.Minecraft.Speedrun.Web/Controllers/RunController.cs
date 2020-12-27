using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Authorize]
    public class RunController : Controller
    {
        private readonly IConfigService _config;
        private readonly IRunFinder _runFinder;
        private readonly IQuickestEventExtender _quickestEventEnhancer;
        private readonly IPlayerEventExtender _playerEventEnhancer;
        private readonly IManualsWriter _manualsWriter;
        private readonly IManualsReader _manualsReader;
        private readonly IRenderStateReader _renderState;
        private readonly IMapRenderModule _renderModule;

        public RunController(
            IRunFinder runFinder,
            IQuickestEventExtender quickestEventEnhancer,
            IPlayerEventExtender playerEventEnhancer,
            IManualsWriter manualsWriter,
            IManualsReader manualsReader,
            IRenderStateReader renderState,
            IMapRenderModule renderModule,
            IConfigService config)
        {
            _config = config;
            _runFinder = runFinder;
            _quickestEventEnhancer = quickestEventEnhancer;
            _playerEventEnhancer = playerEventEnhancer;
            _manualsWriter = manualsWriter;
            _manualsReader = manualsReader;
            _renderState = renderState;
            _renderModule = renderModule;
        }

        [HttpGet("world/{worldname}")]
        public IActionResult Name(string worldname)
        {
            var run = _runFinder.GetByName(worldname);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Extend(run.Import);
            viewmodel.PlayerEvents = _playerEventEnhancer.Extend(run.Import);
            viewmodel.MapRender = _renderState.Get(worldname);
            viewmodel.OverviewerUrl = _config.OverviewerUrl.ToString();

            return View("Index", viewmodel);
        }

        [HttpGet("run/{index}")]
        public IActionResult Index(int index)
        {
            var run = _runFinder.GetByIndex(index);
            var viewmodel = new RunDetailViewModel(run);

            viewmodel.FirstAdvancement = _quickestEventEnhancer.Extend(run.Import);
            viewmodel.PlayerEvents = _playerEventEnhancer.Extend(run.Import);
            viewmodel.MapRender = _renderState.Get(run.Analytics.Worldname);

            return View(viewmodel);
        }

        [HttpGet("world/{worldname}/edit")]
        public IActionResult Edit(string worldname)
        {
            RunManuals manuals = _manualsReader.Get(worldname);
            if (manuals == null)
                manuals = new RunManuals(worldname);

            var viewmodel = new RunEditViewModel(manuals);
            return View(viewmodel);
        }

        [HttpPost("world/{worldname}/edit")]
        public IActionResult Edited(RunEditViewModel model)
        {
            var manuals = new RunManuals(model.Worldname);
            manuals.YoutubeLink = model.YoutubeLink;
            manuals.SpeedrunLink = model.SpeedrunLink;
            manuals.RuntimeInMs = parseRuntime(model.Runtime);

            _manualsWriter.Upsert(manuals);

            return RedirectToAction("Edit", new { worldname = model.Worldname });
        }

        [HttpGet("rendermap/{worldname}")]
        public IActionResult RenderMap(string worldname)
        {
            _renderModule.CreateJob(worldname);
            return RedirectToAction("Name", new { worldname });
        }

        private long? parseRuntime(string runtime)
        {
            var splits = runtime.Split(":");
            if (splits.Length != 4)
                return null;

            if (!int.TryParse(splits[0], out var h))
                return null;

            if (!int.TryParse(splits[1], out var min))
                return null;

            if (!int.TryParse(splits[2], out var sec))
                return null;

            if (!int.TryParse(splits[3], out var ms))
                return null;

            return (long)new TimeSpan(h, min, sec).Add(TimeSpan.FromMilliseconds(ms)).TotalMilliseconds;
        }
    }
}