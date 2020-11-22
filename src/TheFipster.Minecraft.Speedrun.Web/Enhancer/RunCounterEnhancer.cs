using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public class RunCounterEnhancer : IRunCounterEnhancer
    {
        private readonly ITimingStore _timingStore;

        public RunCounterEnhancer(ITimingStore timingStore)
        {
            _timingStore = timingStore;
        }

        public RunCounts Enhance()
        {
            var timings = _timingStore.Get();
            var runCount = new RunCounts();

            foreach (var timing in timings)
            {
                if (timing.PlayTime != TimeSpan.Zero)
                    runCount.Attempts++;

                if (timing.Events.Any(x => x.Section == RunSections.Spawn))
                    runCount.Nether++;

                if (timing.Events.Any(x => x.Section == RunSections.Nether))
                    runCount.Search++;

                if (timing.Events.Any(x => x.Section == RunSections.Fortress))
                    runCount.Fortresses++;

                if (timing.Events.Any(x => x.Section == RunSections.Search))
                    runCount.Stronghold++;

                if (timing.Events.Any(x => x.Section == RunSections.Stronghold))
                    runCount.End++;

                if (timing.Events.Any(x => x.Section == RunSections.TheEnd))
                    runCount.Finished++;
            }

            return runCount;
        }
    }
}
