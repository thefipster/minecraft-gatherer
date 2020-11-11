using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<WorldInfo> Worlds { get; set; }
    }
}
