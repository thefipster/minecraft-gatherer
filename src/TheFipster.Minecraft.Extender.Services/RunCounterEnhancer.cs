using System;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Extender.Services
{
    public class RunCounterEnhancer : IRunCounterEnhancer
    {
        private readonly IAnalyticsStore _analyticsStore;

        public RunCounterEnhancer(IAnalyticsStore analyticsStore)
            => _analyticsStore = analyticsStore;

        public RunCounts Enhance()
        {
            var analytics = _analyticsStore.Get();
            var runCount = new RunCounts();

            foreach (var analytic in analytics)
            {
                if (analytic.Timings.PlayTime != TimeSpan.Zero)
                    runCount.Attempts++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.Spawn))
                    runCount.Nether++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.Nether))
                    runCount.Search++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.Fortress))
                    runCount.Fortresses++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.Search))
                    runCount.Stronghold++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.Stronghold))
                    runCount.End++;

                if (analytic.Timings.Events.Any(x => x.Section == RunSections.TheEnd))
                    runCount.Finished++;
            }

            return runCount;
        }
    }
}
