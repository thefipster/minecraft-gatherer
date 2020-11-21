using System;

namespace TheFipster.Minecraft.Speedrun.Domain.Analytics
{
    public class TimingEvent
    {
        public string PlayerId { get; set; }
        public EventNames Event { get; set; }
        public TimeSpan Time { get; set; }
    }
}
