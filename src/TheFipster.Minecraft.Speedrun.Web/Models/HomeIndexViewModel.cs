using System.Collections.Generic;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
        }

        public RunCounts RunCounts { get; internal set; }
        public IEnumerable<RunHeaderViewModel> LatestRuns { get; internal set; }
    }
}