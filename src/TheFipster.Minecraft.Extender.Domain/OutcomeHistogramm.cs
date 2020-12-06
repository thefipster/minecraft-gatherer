using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Extender.Domain
{
    public class OutcomeHistogramm
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Dictionary<Outcomes, IEnumerable<string>> Outcomes { get; set; }
    }
}
