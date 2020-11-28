using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
        }

        public RunCounts RunCounts { get; internal set; }
        public IEnumerable<RunAnalytics> LatestRuns { get; internal set; }
    }
}