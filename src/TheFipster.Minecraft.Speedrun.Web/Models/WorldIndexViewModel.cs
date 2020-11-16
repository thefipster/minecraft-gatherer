using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class WorldIndexViewModel
    {
        public WorldIndexViewModel()
        {
            Worlds = new List<WorldInfo>();
        }

        public IEnumerable<WorldInfo> Worlds { get; set; }
    }
}
