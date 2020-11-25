using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class LogFinder : ILogFinder
    {
        private readonly IConfigService _config;

        public LogFinder(IConfigService config)
            => _config = config;

        public IEnumerable<string> Find(DateTime date)
        {
            var logs = new List<string>();
            var files = getFiles(date);

            foreach (var file in files)
            {
                var content = File.ReadAllLines(file.FullName);
                logs.AddRange(content);
            }

            return logs;
        }

        private List<FileInfo> getFiles(DateTime date)
        {
            var allLogs = _config.LogLocation.GetFiles("*.log");
            var filenames = allLogs.Select(x => x.Name.Replace(".log", string.Empty));
            var timestamps = new List<long>();

            foreach (var filename in filenames)
                if (int.TryParse(filename, out var timestamp))
                    timestamps.Add(timestamp);

            // Take all logs from the date of the run and for safety reasons subtract/add 12 hours to the start and end respectively
            var start = new DateTimeOffset(date.Date).AddHours(-12).ToUnixTimeSeconds();
            var end = new DateTimeOffset(date.Date).AddHours(36).ToUnixTimeSeconds();

            var validFilenames = timestamps.Where(x => x > start && x < end).Select(y => y.ToString() + ".log");
            var candidates = new List<FileInfo>();
            foreach (var log in allLogs)
                if (validFilenames.Contains(log.Name))
                    candidates.Add(log);

            return candidates;
        }
    }
}
