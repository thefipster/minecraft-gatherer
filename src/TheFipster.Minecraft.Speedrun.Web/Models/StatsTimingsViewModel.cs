using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class StatsTimingsViewModel
    {
        public StatsTimingsViewModel()
            => Stats = new Dictionary<string, IEnumerable<TimingStats>>();

        public Dictionary<string, IEnumerable<TimingStats>> Stats { get; set; }
        public Dictionary<Sections, RunTiming> FastestSections { get; internal set; }
    }
}