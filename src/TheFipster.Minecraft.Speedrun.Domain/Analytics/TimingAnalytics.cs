using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain.Analytics
{
    public class TimingAnalytics
    {
        public TimingAnalytics()
        {
            Events = new List<TimingEvent>();
        }

        public TimingAnalytics(string worldName) : this()
        {
            Worldname = worldName;
        }

        public string Worldname { get; set; }
        public IEnumerable<TimingEvent> Events { get; set; }

        public DateTime StartedOn { get; set; }
        public StartTimeStrategy StartTimeStrategy { get; set; }

        public DateTime? FinishedOn { get; set; }
        public FinishTimeStrategy FinishTimeStrategy { get; set; }
    }
}
