using System;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class TimingEvent
    {
        public TimingEvent()
        {

        }

        public TimingEvent(Sections section)
        {
            Section = section;
        }

        public string FirstPlayerId { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Sections Section { get; set; }
        public bool IsSubSplit { get; set; }
    }
}
