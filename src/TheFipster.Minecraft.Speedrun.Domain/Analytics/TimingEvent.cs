using System;

namespace TheFipster.Minecraft.Speedrun.Domain.Analytics
{
    public class TimingEvent
    {
        public TimingEvent()
        {

        }

        public TimingEvent(RunSections section)
        {
            Section = section;
        }

        public string FirstPlayerId { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RunSections Section { get; set; }
        public bool IsSubSplit { get; set; }
    }
}
