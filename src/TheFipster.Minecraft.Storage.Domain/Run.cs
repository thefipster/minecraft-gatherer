using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Storage.Domain
{
    public class Run
    {
        public RunImport Import { get; set; }
        public RunAnalytics Analytics { get; set; }
        public RunManuals Manuals { get; set; }
    }
}
