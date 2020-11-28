using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules.Components
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly ITimingAnalyzer _timingAnalyzer;
        private readonly IOutcomeAnalyzer _outcomeAnalyzer;

        public AnalyticsModule(
            ITimingAnalyzer timingAnalyzer,
            IOutcomeAnalyzer outcomeAnalyzer)
        {
            _timingAnalyzer = timingAnalyzer;
            _outcomeAnalyzer = outcomeAnalyzer;
        }

        public RunAnalytics Analyze(RunImport run)
        {
            var analytics = new RunAnalytics(run.Worldname);

            analytics.Timings = _timingAnalyzer.Analyze(run);
            analytics.Outcome = _outcomeAnalyzer.Analyze(run);

            return analytics;
        }
    }
}
