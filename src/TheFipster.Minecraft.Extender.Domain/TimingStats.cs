using System;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Domain
{
    public class TimingStats
    {
        public TimingStats() { }

        public TimingStats(MetaFeatures key)
            => Section = key.ToString();

        public string Section { get; set; }
        public TimeSpan Minimum { get; set; }
        public TimeSpan Maximum { get; set; }
        public TimeSpan Average { get; set; }
        public TimeSpan StandardDeviation { get; set; }
    }
}
