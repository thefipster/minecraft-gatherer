using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingStrongholdSectionDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingStrongholdSectionDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);
            var timingEvent = new TimingEvent(Sections.Stronghold);

            if (!appendStartIfPossible(timingEvent))
                return timings;

            if (!appendEndIfPossible(timingEvent))
                return timings;

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var gameEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.EyeSpy);

            if (gameEvent != null)
            {
                timingEvent.Start = gameEvent.Timestamp;
                timingEvent.FirstPlayerId = gameEvent.PlayerId;
                return true;
            }

            return false;
        }

        private bool appendEndIfPossible(TimingEvent timingEvent)
        {
            var gameEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.TheEnd);

            if (gameEvent != null)
            {
                timingEvent.End = gameEvent.Timestamp;
                timingEvent.Time = timingEvent.End - timingEvent.Start;
                return true;
            }

            return false;
        }
    }
}
