using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Domain
{
    public class RunTiming
    {
        public RunTiming() { }

        public RunTiming(Sections section, RunMeta<int> timing, RunAnalytics run)
        {
            Section = section;
            Timing = timing;
            Run = run;
        }

        public Sections Section { get; set; }
        public RunMeta<int> Timing { get; set; }
        public RunAnalytics Run { get; set; }
    }
}
