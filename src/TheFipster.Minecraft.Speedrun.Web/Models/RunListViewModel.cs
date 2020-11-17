using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunListViewModel
    {
        public RunListViewModel()
        {
            Runs = new List<RunInfo>();
        }

        public RunListViewModel(IEnumerable<RunInfo> runs)
        {
            Runs = runs;
        }

        public IEnumerable<RunInfo> Runs { get; set; }
    }
}
