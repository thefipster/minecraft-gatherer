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
        private readonly IPlayerAnalyzer _playerAnalyzer;

        public AnalyticsModule(
            ITimingAnalyzer timingAnalyzer,
            IOutcomeAnalyzer outcomeAnalyzer,
            IPlayerAnalyzer playerAnalyzer)
        {
            _timingAnalyzer = timingAnalyzer;
            _outcomeAnalyzer = outcomeAnalyzer;
            _playerAnalyzer = playerAnalyzer;
        }

        public RunAnalytics Analyze(RunImport run)
        {
            var analytics = new RunAnalytics(run.Worldname);

            analytics.World = run.World;
            analytics.Dimensions = run.Dimensions;
            analytics.Timings = _timingAnalyzer.Analyze(run);
            analytics.Outcome = _outcomeAnalyzer.Analyze(run);
            analytics.Players = _playerAnalyzer.Analyze(run);

            return analytics;
        }
    }
}
