using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class ImportModule : IImportModule
    {
        private readonly IConfigService _config;
        private readonly IServerPropertiesReader _serverPropertiesReader;
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly ILogAnalyzer _logAnalyzer;
        private readonly IEventSplitExtractor _splitExtractor;
        private readonly IEventPlayerExtractor _eventPlayerExtractor;
        private readonly IStatsPlayerExtractor _statsPlayerExtractor;
        private readonly IAchievementExtractor _achievementExtractor;
        private readonly IStatsExtractor _statsExtractor;
        private readonly IValidityChecker _validityChecker;
        private readonly IOutcomeChecker _outcomeChecker;
        private readonly IRunStore _runStore;
        private readonly ILogger<ImportModule> _logger;

        public ImportModule(
            IConfigService config,
            IServerPropertiesReader serverPropertiesReader,
            IWorldFinder worldFinder,
            IWorldLoader worldLoader,
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            ILogAnalyzer logAnalyzer,
            IEventSplitExtractor splitExtractor,
            IEventPlayerExtractor eventPlayerExtractor,
            IStatsPlayerExtractor statsPlayerExtractor,
            IStatsExtractor statsExtractor,
            IAchievementExtractor achievementExtractor,
            IValidityChecker validityChecker,
            IOutcomeChecker outcomeChecker,
            IRunStore runStore,
            ILogger<ImportModule> logger)
        {
            _config = config;
            _serverPropertiesReader = serverPropertiesReader;
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _logAnalyzer = logAnalyzer;
            _splitExtractor = splitExtractor;
            _eventPlayerExtractor = eventPlayerExtractor;
            _statsPlayerExtractor = statsPlayerExtractor;
            _achievementExtractor = achievementExtractor;
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
            var activeWorld = string.Empty;

            try
            {
                activeWorld = _serverPropertiesReader.Read().LevelName;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Import: Can't read server properties, active world is not filtered.");
            }

            foreach (var candiate in candidates)
            {
                if (candiate.Name == activeWorld)
                {
                    _logger.LogDebug($"Candidate Check: Skipping world {candiate.Name} because it is currently active.");
                    continue;
                }

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

            foreach (var run in runs.OrderBy(x => x.World.CreatedOn))
            {
                _logger.LogDebug($"Run Load: Enhancing information for world {run.Id}.");

                run.Stats = _statsExtractor.Extract(run.World.Name);
                run.Achievements = _achievementExtractor.Extract(run.World);

                try
                {
                    run.Logs = gatherLogs(run.World);
                    run.Logs = _logAnalyzer.Analyze(run.Logs);
                    run.Players = _eventPlayerExtractor.Extract(run.Logs.Events);
                    run.Splits = _splitExtractor.Extract(run.Logs.Events);
                    run.Outcome = _outcomeChecker.Check(run);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, $"Run Load: Enhancement for {run.Id} via logs failed.");
                    run.Players = _statsPlayerExtractor.Extract(run.Stats);
                }

                run.Validity = _validityChecker.Check(run);

                _logger.LogDebug($"Run Load: Adding world {run.Id} to the store.");
                if (overwrite)
                {
                    _runStore.Update(run);
                }
                else
                {
                    var currentIndex = _runStore.Count();
                    run.Index = _config.InitialRunIndex + currentIndex + 1;
                    _runStore.Add(run);
                }
            }

            return runs;
        }

        private bool runExistInStore(DirectoryInfo candiate)
            => _runStore.Exists(candiate.Name);

        private ServerLog gatherLogs(WorldInfo world)
        {
            var allLogs = _logFinder.Find(world.CreatedOn).ToList();
            var parsedLogs = _logParser.Read(allLogs, world.CreatedOn);
            var orderedLogs = parsedLogs.OrderBy(x => x.Timestamp);
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
