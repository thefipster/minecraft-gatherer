using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class WorldIndexViewModel
    {
        public IEnumerable<WorldInfo> Worlds { get; set; }
    }
}
