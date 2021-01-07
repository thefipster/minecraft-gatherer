using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Extender.Domain
{
    public class OutcomeHistogram
    {
        public OutcomeHistogram()
        {
            Periods = new List<int>();
            Attempts = new List<int>();
            Labels = new List<string>();
            PeriodsStartedOn = new List<DateTime>();
            Untouched = new List<double>();
            Unknown = new List<double>();
            Discarded = new List<double>();
            Started = new List<double>();
            Nether = new List<double>();
            Fortress = new List<double>();
            Search = new List<double>();
            Stronghold = new List<double>();
            End = new List<double>();
            Finished = new List<double>();
        }

        public OutcomeHistogram(Periods period)
            : this()
            => Period = period.ToString();

        public string Period { get; set; }
        public ICollection<int> Periods { get; set; }
        public ICollection<int> Attempts { get; set; }
        public ICollection<DateTime> PeriodsStartedOn { get; set; }
        public ICollection<double> Untouched { get; set; }
        public ICollection<double> Unknown { get; set; }
        public ICollection<double> Discarded { get; set; }
        public ICollection<double> Started { get; set; }
        public ICollection<double> Nether { get; set; }
        public ICollection<double> Fortress { get; set; }
        public ICollection<double> Search { get; set; }
        public ICollection<double> Stronghold { get; set; }
        public ICollection<double> End { get; set; }
        public ICollection<double> Finished { get; set; }
        public ICollection<string> Labels { get; set; }
    }
}
