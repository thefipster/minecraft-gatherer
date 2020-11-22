using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly ITimingStore _timingStore;
        private readonly ITimingAnalyzer _timingAnalyser;

        public AnalyticsModule(ITimingStore timingStore, ITimingAnalyzer timingAnalyser)
        {
            _timingStore = timingStore;
            _timingAnalyser = timingAnalyser;
        }

        public RunAnalytics Analyze(RunInfo run)
        {
            var runAnalytics = new RunAnalytics();

            runAnalytics.WorldName = run.World.Name;
            runAnalytics.Timings = analyzeTimings(run);

            return runAnalytics;
        }

        private TimingAnalytics analyzeTimings(RunInfo run)
        {
            var timings = _timingAnalyser.Analyse(run);
            storeTimings(timings);
            return timings;
        }

        private void storeTimings(TimingAnalytics timings)
        {
            if (_timingStore.Exists(timings.Worldname))
                _timingStore.Update(timings);
            else
                _timingStore.Add(timings);
        }
    }
}
