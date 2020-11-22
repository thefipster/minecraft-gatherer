using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class ImportIndexViewModel
    {
        public ImportIndexViewModel()
        {
            Runs = new List<RunInfo>();
        }

        public IEnumerable<RunInfo> Runs { get; set; }
    }
}
