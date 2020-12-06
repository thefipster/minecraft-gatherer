using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Modules
{
    public class ImportModule : IImportRunModule
    {
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly IDimensionLoader _dimensionLoader;
        private readonly IAchievementExtractor _achievementExtractor;
        private readonly IStatsExtractor _statsExtractor;
        private readonly IPlayerNbtReader _playerNbtReader;
        private readonly IImportStore _runStore;

        private readonly ILogger<ImportModule> _logger;

        public ImportModule(
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            IDimensionLoader dimensionLoader,
            IAchievementExtractor achievementExtractor,
            IStatsExtractor statsExtractor,
            IPlayerNbtReader playerNbtReader,
            IImportStore runImportStore,
            ILogger<ImportModule> logger)
        {
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _dimensionLoader = dimensionLoader;
            _achievementExtractor = achievementExtractor;
            _statsExtractor = statsExtractor;
            _playerNbtReader = playerNbtReader;
            _runStore = runImportStore;
            _logger = logger;
        }

        public RunImport Import(WorldInfo world)
        {
            _logger.LogInformation($"Import started for world {world.Name}.");

            var run = new RunImport(world);
            run.Dimensions = _dimensionLoader.Load(run.World);
            run.Stats = _statsExtractor.Extract(run.World);
            run.Achievements = _achievementExtractor.Extract(run.World);
            run.Logs = gatherLogs(run);
            run.EndScreens = _playerNbtReader.Read(run.World);

            _logger.LogInformation($"Import finished for world {world.Name}.");
            return run;
        }

        private void saveToStorage(RunImport run)
        {
            if (_runStore.Exists(run.World.Name))
            {
                _runStore.Update(run);
                _logger.LogDebug($"Import store updated run {run.Worldname}.");
            }
            else
            {
                _runStore.Insert(run);
                _logger.LogDebug($"Import store created run {run.Worldname}.");
            }
        }

        private IEnumerable<LogLine> gatherLogs(RunImport run)
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
                return Enumerable.Empty<LogLine>();
            }
        }
    }
}
