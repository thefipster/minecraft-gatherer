using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class Split
    {
        public Split()
        {

        }

        public Split(SplitTypes type, TimeSpan duration)
        {
            Type = type;
            Timestamp = duration;
        }

        public SplitTypes Type { get; set; }
        public TimeSpan Timestamp { get; set; }
    }
}
