using System;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogArchiveMeta
    {
        public LogArchiveMeta(string logArchivePath)
        {
            ArchivePath = logArchivePath;
            ProcessedOn = DateTime.UtcNow;
        }

        public string ArchivePath { get; set; }
        public DateTime ProcessedOn { get; set; }
    }
}
