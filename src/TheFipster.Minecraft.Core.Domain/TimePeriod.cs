using System;

namespace TheFipster.Minecraft.Core.Domain
{
    public class TimePeriod
    {
        public TimePeriod() { }

        public TimePeriod(DateTime start, DateTime end, string label)
        {
            Start = start;
            End = end;
            Label = label;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Label { get; set; }
    }
}
