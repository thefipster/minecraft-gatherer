using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunListViewModel
    {
        public RunListViewModel()
        {
            Runs = new List<RunInfo>();
            Timings = new List<TimingAnalytics>();
        }

        public RunListViewModel(IEnumerable<TimingAnalytics> timings) : this()
        {
            Timings = timings;
        }

        public RunListViewModel(IList<RunInfo> runs) : this()
        {
            Runs = runs;
        }

        public IList<RunInfo> Runs { get; set; }
        public IEnumerable<TimingAnalytics> Timings { get; internal set; }
    }
}
