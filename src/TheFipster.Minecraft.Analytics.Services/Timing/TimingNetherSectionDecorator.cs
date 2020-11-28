using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingNetherSectionDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingNetherSectionDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);
            var timingEvent = new TimingEvent(RunSections.Nether);

            if (!appendStartIfPossible(timingEvent))
                return timings;

            if (!appendEndIfPossible(timingEvent))
                return timings;

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var weNeedToGoDeeperEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.WeNeedToGoDeeper);

            if (weNeedToGoDeeperEvent != null)
            {
                timingEvent.Start = weNeedToGoDeeperEvent.Timestamp;
                timingEvent.FirstPlayerId = weNeedToGoDeeperEvent.PlayerId;
                return true;
            }

            return false;
        }

        private bool appendEndIfPossible(TimingEvent timingEvent)
        {
            var blazePowderEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.GotEnderEye);

            if (blazePowderEvent != null)
            {
                timingEvent.End = blazePowderEvent.Timestamp;
                timingEvent.Time = timingEvent.End - timingEvent.Start;
                return true;
            }

            return false;
        }
    }
}
