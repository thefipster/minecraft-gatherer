using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
        }

        public IEnumerable<RunInfo> LatestRuns { get; internal set; }
        public RunCounts RunCounts { get; internal set; }
    }
}