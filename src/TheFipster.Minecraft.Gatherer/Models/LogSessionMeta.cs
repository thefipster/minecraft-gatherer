using System;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogSessionMeta
    {
        public LogSessionMeta(string archivePath, string logPath, DateTime start, DateTime end, int entries)
        {
            OriginalArchive = archivePath;
            LogPath = logPath;
            Start = start;
            End = end;
            Entries = entries;
            ProcessedOn = DateTime.UtcNow;
        }

        public string OriginalArchive { get; set; }
        public string LogPath { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Entries { get; set; }
        public DateTime ProcessedOn { get; set; }
    }
}
