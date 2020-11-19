using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class Timings
    {
        public Timings()
        {
            Splits = new List<Split>();
            Reasons = new List<string>();
        }

        public Timings(DateTime baseTime) : this()
        {
            StartedOn = baseTime;
            IsValid = true;
        }

        public Timings(string reason) : this()
        {
            Reasons.Add(reason);
        }

        public Timings(DateTime baseTime, string reason) : this(reason)
        {
            StartedOn = baseTime;
        }

        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public bool IsValid { get; set; }
        public List<Split> Splits { get; set; }
        public List<string> Reasons { get; set; }
    }
}
