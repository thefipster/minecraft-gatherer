using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class ImportRunModule : IImportRunModule
    {
        private readonly IConfigService _config;
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

        private readonly ILogger<ImportRunModule> _logger;

        public ImportRunModule(
            IConfigService config,
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
            ILogger<ImportRunModule> logger)
        {
            _config = config;
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
        }

        public RunInfo Import(WorldInfo world)
        {
            _logger.LogInformation($"Import started for world {world.Name}.");

            var run = new RunInfo(world);
            attachInformationsTo(run);
            enhanceInformationOf(run);
            attachConclusionsOf(run);
            saveToStorage(run);

            _logger.LogInformation($"Import finished for world {world.Name}.");
            return run;
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
            attachPlayersFromEventsIfPossible(run);
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
                _logger.LogDebug($"Import store new run {run.Id}.");
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

        private void attachEventsFromLogsIfPossible(RunInfo run)
        {
            if (run.Logs != null)
            {
                var logEvents = _logEventExtractor.Extract(run.Logs);
                run.Events.AddRange(logEvents);
            }
        }

        private void attachPlayersFromEventsIfPossible(RunInfo run)
        {
            var playersFromEvents = _eventPlayerExtractor.Extract(run.Events);
            foreach (var player in playersFromEvents)
                if (!run.Players.Any(x => x.Id == player.Id))
                    run.Players.Add(player);
        }
    }
}
