using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class LogLoader : ILogLoader
    {
        private readonly ILogFinder _logFinder;
        private readonly ILogParser _logParser;
        private readonly ILogTrimmer _logTrimmer;
        private readonly ILogger<LogLoader> _logger;

        public LogLoader(
            ILogFinder logFinder,
            ILogParser logParser,
            ILogTrimmer logTrimmer,
            ILogger<LogLoader> logger)
        {
            _logFinder = logFinder;
            _logParser = logParser;
            _logTrimmer = logTrimmer;
            _logger = logger;
        }

        public IEnumerable<LogLine> Load(RunImport import)
        {
            try
            {
                var allLogs = _logFinder.Find(import.World.CreatedOn);
                var parsedLogs = _logParser.Read(allLogs, import.World.CreatedOn);
                var orderedLogs = parsedLogs.OrderBy(x => x.Timestamp);
                var trimmedLog = _logTrimmer.Trim(parsedLogs, import.World);
                return trimmedLog;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, $"Run Load: Reading logs failed.");
                import.Problems.Add(new Problem("Logs are not readable.", ex.Message));
                return Enumerable.Empty<LogLine>();
            }
        }
    }
}
