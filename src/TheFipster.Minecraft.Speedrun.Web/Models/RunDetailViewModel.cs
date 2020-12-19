using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Manual.Domain;
using TheFipster.Minecraft.Storage.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunDetailViewModel
    {
        public RunDetailViewModel(Run run)
        {
            Import = run.Import;
            Analytics = run.Analytics;
            Manuals = run.Manuals;
            Locations = run.Locations.Keys.ToList();
        }

        public IEnumerable<FirstEvent> FirstAdvancement { get; internal set; }
        public Dictionary<string, IEnumerable<RunEvent>> PlayerEvents { get; internal set; }

        public RunImport Import { get; internal set; }
        public RunAnalytics Analytics { get; internal set; }
        public RunManuals Manuals { get; internal set; }
        public IEnumerable<Locations> Locations { get; }
    }
}
