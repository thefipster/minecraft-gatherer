using System;
using System.Collections.Generic;
using System.Linq;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class ServerLog
    {
        public ServerLog()
        {
            Events = new List<LogEvent>();
        }

        public ServerLog(IEnumerable<LogLine> lines)
            : this()
        {
            Lines = lines.ToList();
        }

        public List<LogLine> Lines { get; set; }
        public List<LogEvent> Events { get; set; }
        public DateTime AnalyzedOn { get; set; }
    }
}
