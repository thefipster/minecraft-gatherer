using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class WorldController : Controller
    {
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly IPlayerStore _playerStore;
        private readonly ILogger<WorldController> _logger;

        public WorldController(IWorldFinder worldFinder, IWorldLoader worldLoader, ILogFinder logFinder, ILogParser logParser, ILogTrimmer logTrimmer, IPlayerStore playerStore, ILogger<WorldController> logger)
        {
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _playerStore = playerStore;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var worlds = new List<WorldInfo>();
            var candidates = _worldFinder.Find();
            foreach (var candiate in candidates)
            {
                try
                {
                    var world = _worldLoader.Load(candiate);
                    worlds.Add(world);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Couldn't load world because an exception was thrown.");
                }
            }

            foreach (var world in worlds)
            {
                var logs = _logFinder.Find(world.CreatedOn).ToList();
                var parsedLogs = _logParser.Read(logs, world.CreatedOn).ToList();
                var trimmedLog = _logTrimmer.Trim(parsedLogs, world).ToList();

                world.Logs = trimmedLog;
            }

            var viewmodel = new WorldIndexViewModel
            {
                Worlds = worlds
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