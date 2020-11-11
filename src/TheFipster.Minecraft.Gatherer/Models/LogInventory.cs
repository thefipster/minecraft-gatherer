using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogInventory
    {
        public DateTime LastScannedOn { get; set; }

        public string LastScannedArchive { get; set; }

        public List<LogArchiveMeta> Archives { get; set; }

        public List<LogSessionMeta> Sessions { get; set; }
    }
}
