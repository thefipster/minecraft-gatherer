using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
        }

        public ServerProperties ServerProperties { get; internal set; }
        public IEnumerable<RunInfo> LatestRuns { get; internal set; }
    }
}