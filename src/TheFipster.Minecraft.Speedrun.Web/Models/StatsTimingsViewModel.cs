using System.Collections.Generic;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class StatsTimingsViewModel
    {
        public StatsTimingsViewModel()
            => Stats = new Dictionary<string, IEnumerable<TimingStats>>();

        public Dictionary<string, IEnumerable<TimingStats>> Stats { get; set; }
    }
}