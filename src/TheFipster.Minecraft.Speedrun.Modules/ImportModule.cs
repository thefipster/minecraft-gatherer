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
        private readonly ILogEventExtractor _logEventExtractor;
        private readonly IEventTimingExtractor _splitExtractor;
        private readonly IEventPlayerExtractor _eventPlayerExtractor;
        private readonly IStatsPlayerExtractor _statsPlayerExtractor;
        private readonly IAchievementEventExtractor _achievementExtractor;
        private readonly IStatsExtractor _statsExtractor;
        private readonly IPlayerNbtReader _playerNbtReader;
        private readonly IValidityChecker _validityChecker;
        private readonly IOutcomeChecker _outcomeChecker;
        private readonly IRunStore _runStore;

        private readonly ILogger<ImportModule> _logger;

        private readonly string _activeWorld;

        public ImportModule(
            IConfigService config,
            IServerPropertiesReader serverPropertiesReader,
            IWorldFinder worldFinder,
            IWorldLoader worldLoader,
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            ILogEventExtractor logEventExtractor,
            IEventTimingExtractor splitExtractor,
            IEventPlayerExtractor eventPlayerExtractor,
            IStatsPlayerExtractor statsPlayerExtractor,
            IStatsExtractor statsExtractor,
            IAchievementEventExtractor achievementExtractor,
            IPlayerNbtReader playerNbtReader,
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
            _logEventExtractor = logEventExtractor;
            _splitExtractor = splitExtractor;
            _eventPlayerExtractor = eventPlayerExtractor;
            _statsPlayerExtractor = statsPlayerExtractor;
            _achievementExtractor = achievementExtractor;
            _statsExtractor = statsExtractor;
            _playerNbtReader = playerNbtReader;
            _validityChecker = validityChecker;
            _outcomeChecker = outcomeChecker;
            _runStore = runStore;
            _logger = logger;

            _activeWorld = tryFindActiveWorld();
        }

        public IEnumerable<RunInfo> Import(bool overwrite = false)
        {
            List<RunInfo> runs = findRunsToImport(overwrite);

            foreach (var run in runs.OrderBy(x => x.World.CreatedOn))
            {
                _logger.LogDebug($"Run Load: Started processing of run {run.Id}.");

                attachInformationsTo(run);
                enhanceInformationOf(run);
                attachConclusionsOf(run);
                saveToStorage(run);

                _logger.LogDebug($"Run Load: Finished processing of run {run.Id}.");
            }

            return runs;
        }

        private string tryFindActiveWorld()
        {
            var activeWorld = string.Empty;
            try
            {
                activeWorld = _serverPropertiesReader.Read().LevelName;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Import: Can't read server properties, active world is not filtered.");
            }

            return activeWorld;
        }

        private List<RunInfo> findRunsToImport(bool overwrite)
        {
            var runs = new List<RunInfo>();
            var candidates = _worldFinder.Find();

            foreach (var candiate in candidates)
            {
                if (candiate.Name == _activeWorld)
                {
                    _logger.LogInformation($"Candidate Check: Skipping world {candiate.Name} because it is currently active.");
                    continue;
                }

                if (!overwrite && _runStore.Exists(candiate.Name))
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

            _logger.LogDebug($"Candidate Check: Completed. Returning {runs.Count()} runs.");
            return runs;
        }

        private void attachInformationsTo(RunInfo run)
        {
            run.Stats = _statsExtractor.Extract(run.World.Name).ToList();
            run.Players = _statsPlayerExtractor.Extract(run.Stats).ToList();
            var achievementEvents = _achievementExtractor.Extract(run.World);
            run.Events.AddRange(achievementEvents);
            run.Logs = gatherLogs(run);
            run.EndScreens = _playerNbtReader.Read(run.World);

            _logger.LogDebug($"Run Load: Attached information to run {run.Id}.");
        }

        private void enhanceInformationOf(RunInfo run)
        {
            attachEventsFromLogsIfPossible(run);
            attachPlayersFromEventIfMissing(run);
            run.Timings = _splitExtractor.Extract(run);

            _logger.LogDebug($"Run Load: Enhanced information of run {run.Id}.");
        }

        private void attachConclusionsOf(RunInfo run)
        {
            run.Outcome = _outcomeChecker.Check(run);
            run.Validity = _validityChecker.Check(run);

            _logger.LogDebug($"Run Load: Concluded findings on run {run.Id}.");
        }

        private void saveToStorage(RunInfo run)
        {
            if (_runStore.Exists(run.World.Name))
            {
                _runStore.Update(run);
            }
            else
            {
                var currentIndex = _runStore.Count();
                run.Index = _config.InitialRunIndex + currentIndex + 1;
                _runStore.Add(run);
            }

            _logger.LogDebug($"Run Load: Stored run {run.Id}.");
        }

        private IEnumerable<LogLine> gatherLogs(RunInfo run)
        {
            try
            {
                var allLogs = _logFinder.Find(run.World.CreatedOn).ToList();
                var parsedLogs = _logParser.Read(allLogs, run.World.CreatedOn);
                var orderedLogs = parsedLogs.OrderBy(x => x.Timestamp);
                var trimmedLog = _logTrimmer.Trim(parsedLogs, run.World);
                return trimmedLog;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, $"Run Load: Reading logs failed.");
                run.Problems.Add(new Problem("Logs are not readable.", ex.Message));
                return null;
            }
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
                _logger.LogInformation(ex, "Couldn't load world.");
                return false;
            }
        }

        private void attachEventsFromLogsIfPossible(RunInfo run)
        {
            if (run.Logs != null)
            {
                var logEvents = _logEventExtractor.Extract(run.Logs);
                run.Events.AddRange(logEvents);
            }
        }

        private void attachPlayersFromEventIfMissing(RunInfo run)
        {
            var playersFromEvents = _eventPlayerExtractor.Extract(run.Events);
            foreach (var player in playersFromEvents)
                if (!run.Players.Any(x => x.Id == player.Id))
                    run.Players.Add(player);
        }
    }
}
