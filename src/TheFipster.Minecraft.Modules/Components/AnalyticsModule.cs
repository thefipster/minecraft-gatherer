using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules.Components
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly ITimingAnalyzer _timingAnalyzer;

        public AnalyticsModule(ITimingAnalyzer timingAnalyzer)
        {
            _timingAnalyzer = timingAnalyzer;
        }

        public RunAnalytics Analyze(RunImport run)
        {
            var analytics = new RunAnalytics(run.Worldname);

            analytics.Timings = _timingAnalyzer.Analyse(run);

            return analytics;
        }
    }
}
