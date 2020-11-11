using System.Collections.Generic;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogProcessResult
    {
        public LogArchiveMeta Archive { get; set; }
        public List<LogSessionMeta> Sessions { get; set; }
    }
}
