using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class WorldIndexViewModel
    {
        public WorldIndexViewModel()
        {
            Runs = new List<RunInfo>();
        }

        public IEnumerable<RunInfo> Runs { get; set; }
    }
}
