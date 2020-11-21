using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class Split
    {
        public Split()
        {

        }

        public Split(SplitTypes type, DateTime timestamp)
        {
            Type = type;
            Timestamp = timestamp;
        }

        public SplitTypes Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
