using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class ImportIndexViewModel
    {
        public ImportIndexViewModel()
        {
            Runs = new List<RunImport>();
        }

        public IEnumerable<RunImport> Runs { get; set; }
    }
}
