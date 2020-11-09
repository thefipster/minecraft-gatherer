using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogSession
    {
        public IEnumerable<LogLine> Lines { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string OriginalArchive { get; set; }
    }
}
