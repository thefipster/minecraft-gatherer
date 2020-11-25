using System;

namespace TheFipster.Minecraft.Import.Domain
{
    public class Achievement
    {
        public Achievement() { }

        public Achievement(string name, DateTime timestamp)
        {
            Event = name;
            Timestamp = timestamp;
        }

        public string Event { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
            => $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {Event}";
    }
}
