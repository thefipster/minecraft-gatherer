using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class LogTrimmer : ILogTrimmer
    {
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
    }
}
