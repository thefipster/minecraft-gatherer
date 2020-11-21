using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain.Analytics
{
    public class TimingAnalytics
    {
        public string Worldname { get; set; }
        public IEnumerable<TimingEvent> Events { get; set; }
    }
}
