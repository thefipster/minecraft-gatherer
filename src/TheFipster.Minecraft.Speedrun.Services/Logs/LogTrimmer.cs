using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogTrimmer : ILogTrimmer
    {
        private TimeSpan gracePeriod = TimeSpan.FromSeconds(5);

        private string[] possibleEnds = new[]
        {
            "Stopping server",
            "Saving worlds",
            "Exception stopping the server",
            "Thread RCON Listener stopped",
            "shutting down",
            "[Rcon: Stopping the server]"
        };

        public IEnumerable<LogLine> Trim(IEnumerable<LogLine> lines, WorldInfo world)
        {
            var matches = lines.Where(x => x.Message.Contains(world.Name));
            var start = matches.Where(x => x.Message.ToLower().Contains("preparing level"));
            var end = matches.Where(x => x.Message.ToLower().Contains("threadedanvilchunkstorage"));

            if (start.Any() && end.Any())
            {
                var startTime = start.Min(x => x.Timestamp);
                var endTime = end.Max(x => x.Timestamp);

                return lines.Where(x => x.Timestamp >= startTime && x.Timestamp <= endTime);
            }

            throw new Exception("There are no logs for this run.");
        }

        private IEnumerable<LogLine> trimStart(IEnumerable<LogLine> lines, WorldInfo world)
        {
            var matches = lines.Where(x => x.Message.Contains(world.Name));



            return Enumerable.Empty<LogLine>();
        }

        private IEnumerable<LogLine> trimEnd(IEnumerable<LogLine> lines, WorldInfo world)
        {
            var lineCounter = 0;

            foreach (var line in lines)
            {
                foreach (var ending in possibleEnds)
                    if (line.Message.Contains(ending))
                        return lines.Take(lineCounter);

                lineCounter++;
            }

            return lines;
        }
    }
}
