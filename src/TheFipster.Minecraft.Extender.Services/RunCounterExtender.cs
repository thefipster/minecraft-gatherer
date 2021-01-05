using System;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class RunCounterExtender : IRunCounterExtender
    {
        private readonly IAnalyticsReader _analyticsReader;

        public RunCounterExtender(IAnalyticsReader analyticsReader)
            => _analyticsReader = analyticsReader;

        public RunCounts Extend()
        {
            var analytics = _analyticsReader.Get();
            var runCount = new RunCounts();

            foreach (var analytic in analytics)
            {
                if (analytic.Outcome == Outcomes.Unknown
                    || analytic.Outcome == Outcomes.Untouched)
                    continue;

                if (analytic.Timings.PlayTime != TimeSpan.Zero)
                    runCount.Attempts++;

                if (analytic.Players.Count() > 1)
                    runCount.Started++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.Spawn))
                    runCount.Nether++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.Nether))
                    runCount.Search++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.Fortress))
                    runCount.Fortresses++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.Search))
                    runCount.Stronghold++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.Stronghold))
                    runCount.End++;

                if (analytic.Timings.Events.Any(x => x.Section == Sections.TheEnd))
                    runCount.Finished++;
            }

            return runCount;
        }
    }
}
