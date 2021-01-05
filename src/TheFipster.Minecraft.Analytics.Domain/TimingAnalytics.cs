using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class TimingAnalytics
    {
        public TimingAnalytics()
        {
            Events = new List<TimingEvent>();
        }

        public TimingAnalytics(string worldName) : this()
        {
            Id = worldName;
            Worldname = worldName;
        }

        public string Id { get; set; }
        public string Worldname { get; set; }
        public IList<TimingEvent> Events { get; set; }

        public DateTime StartedOn { get; set; }
        public StartTimeStrategy StartTimeStrategy { get; set; }

        public DateTime? FinishedOn { get; set; }
        public FinishTimeStrategy FinishTimeStrategy { get; set; }

        public TimeSpan RunTime { get; set; }
        public TimeSpan PlayTime { get; set; }
        public bool ManualOverride { get; set; }
    }
}
