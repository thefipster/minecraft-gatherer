using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunListViewModel
    {
        public RunListViewModel()
            => Runs = new List<RunAnalytics>();

        public RunListViewModel(IEnumerable<RunAnalytics> runs)
            => Runs = runs;

        public IEnumerable<RunAnalytics> Runs { get; set; }
    }
}
