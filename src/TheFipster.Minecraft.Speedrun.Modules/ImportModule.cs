using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class ImportModule : IImportModule
    {
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly ILogAnalyzer _logAnalyzer;
        private readonly IEventSplitExtractor _splitExtractor;
        private readonly IEventPlayerExtractor _playerExtractor;
        private readonly IPlayerStatsExtractor _statsExtractor;
        private readonly IValidityChecker _validityChecker;
        private readonly IOutcomeChecker _outcomeChecker;
        private readonly IRunStore _runStore;
        private readonly ILogger<ImportModule> _logger;

        public ImportModule(
            IPlayerStore playerStore,
            IWorldFinder worldFinder,
            IWorldLoader worldLoader,
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            ILogAnalyzer logAnalyzer,
            IEventSplitExtractor splitExtractor,
            IEventPlayerExtractor playerExtractor,
            IPlayerStatsExtractor statsExtractor,
            IValidityChecker validityChecker,
            IOutcomeChecker outcomeChecker,
            IRunStore runStore,
            ILogger<ImportModule> logger)
        {
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _logAnalyzer = logAnalyzer;
            _splitExtractor = splitExtractor;
            _playerExtractor = playerExtractor;
            _statsExtractor = statsExtractor;
            _validityChecker = validityChecker;
            _outcomeChecker = outcomeChecker;
            _runStore = runStore;
            _logger = logger;
        }

        public IEnumerable<RunInfo> Import(bool overwrite = false)
        {
            var runs = new List<RunInfo>();
            var candidates = _worldFinder.Find();

            foreach (var candiate in candidates)
            {
                if (!overwrite && runExistInStore(candiate))
                {
                    _logger.LogDebug($"Candidate Check: Skipping world {candiate.Name} because it was already imported.");
                    continue;
                }

                if (tryLoadWorldFolder(candiate, out var run))
                {
                    _logger.LogDebug($"Candidate Check: Loading world {candiate.Name}.");
                    runs.Add(run);
                }
            }

            foreach (var run in runs)
            {
                _logger.LogDebug($"Enhancing information for world {run.Id}.");

                run.Logs = gatherLogs(run.World);
                run.Logs = _logAnalyzer.Analyze(run.Logs);
                run.Players = _playerExtractor.Extract(run.Logs.Events);
                run.Splits = _splitExtractor.Extract(run.Logs.Events);
                run.Stats = _statsExtractor.Extract(run.World.Name);
                run.Validity = _validityChecker.Check(run);
                run.Outcome = _outcomeChecker.Check(run);

                _logger.LogDebug($"Adding world {run.Id} to the store.");

                if (overwrite)
                    _runStore.Update(run);
                else
                    _runStore.Add(run);
            }

            return runs;
        }

        private bool runExistInStore(DirectoryInfo candiate)
            => _runStore.Exists(candiate.Name);

        private ServerLog gatherLogs(WorldInfo world)
        {
            var allLogs = _logFinder.Find(world.CreatedOn);
            var parsedLogs = _logParser.Read(allLogs, world.CreatedOn);
            var trimmedLog = _logTrimmer.Trim(parsedLogs, world);

            return new ServerLog(trimmedLog);
        }

        private bool tryLoadWorldFolder(DirectoryInfo candiate, out RunInfo run)
        {
            run = new RunInfo();

            try
            {
                run.World = _worldLoader.Load(candiate);
                run.Id = run.World.Name;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Couldn't load world because an exception was thrown.");
                return false;
            }
        }
    }
}
