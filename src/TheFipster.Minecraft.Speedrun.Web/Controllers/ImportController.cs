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
    public class ImportController : Controller
    {
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly ILogAnalyzer _logAnalyzer;
        private readonly IPlayerStore _playerStore;
        private readonly IEventSplitExtractor _splitExtractor;
        private readonly IEventPlayerExtractor _playerExtractor;
        private readonly ILogger<ImportController> _logger;

        public ImportController(
            IPlayerStore playerStore,
            IWorldFinder worldFinder,
            IWorldLoader worldLoader,
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            ILogAnalyzer logAnalyzer,
            IEventSplitExtractor splitExtractor,
            IEventPlayerExtractor playerExtractor,
            ILogger<ImportController> logger)
        {
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _logAnalyzer = logAnalyzer;
            _playerStore = playerStore;
            _splitExtractor = splitExtractor;
            _playerExtractor = playerExtractor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var runs = new List<RunInfo>();
            var candidates = _worldFinder.Find();
            foreach (var candiate in candidates)
            {
                try
                {
                    var run = new RunInfo();
                    run.World = _worldLoader.Load(candiate);
                    runs.Add(run);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "Couldn't load world because an exception was thrown.");
                }
            }

            foreach (var run in runs)
            {
                var allLogs = _logFinder.Find(run.World.CreatedOn).ToList();
                var parsedLogs = _logParser.Read(allLogs, run.World.CreatedOn).ToList();
                var trimmedLog = _logTrimmer.Trim(parsedLogs, run.World).ToList();

                var logs = new ServerLog(trimmedLog);
                logs = _logAnalyzer.Analyze(logs);
                run.Logs = logs;

                var players = _playerExtractor.Extract(run.Logs.Events);
                run.Players = players;

                var splits = _splitExtractor.Extract(run.Logs.Events);
                run.Splits = splits;
            }

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