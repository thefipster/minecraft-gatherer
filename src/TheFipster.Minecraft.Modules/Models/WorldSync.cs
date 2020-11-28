using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules.Models
{
    public class WorldSync
    {
        public WorldSync() { }

        public WorldSync(RunImport import, RunAnalytics analytic)
        {
            Worldname = import.Worldname;
            Import = import;
            Analytics = analytic;
        }

        public string Worldname { get; set; }

        public RunImport Import { get; set; }
        public RunAnalytics Analytics { get; set; }
    }
}
