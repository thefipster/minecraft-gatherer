using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Modules.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class ImportIndexViewModel
    {
        public ImportIndexViewModel()
            => Sync = Enumerable.Empty<WorldSync>();

        public IEnumerable<WorldSync> Sync { get; internal set; }
    }
}
