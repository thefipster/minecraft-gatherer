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
            lines = trimStart(lines, world);
            lines = trimEnd(lines, world);

            return lines;
        }

        private IEnumerable<LogLine> trimStart(IEnumerable<LogLine> lines, WorldInfo world)
        {
            var timeAdjust = world.WrittenOn - world.WrittenUtcOn;
            var logBegin = world.CreatedOn + timeAdjust + gracePeriod;

            return lines.Where(line => line.Timestamp > logBegin);
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
