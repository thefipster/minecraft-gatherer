using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;


namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingTheEndSectionDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingTheEndSectionDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunImport run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.TheEnd);

            if (!appendStartIfPossible(timingEvent))
                return timings;

            if (timings.FinishedOn.HasValue)
            {
                timingEvent.End = timings.FinishedOn.Value;
                timingEvent.Time = timingEvent.End - timingEvent.Start;
            }
            else
            {
                return timings;
            }

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var gameEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.TheEnd);

            if (gameEvent != null)
            {
                timingEvent.Start = gameEvent.Timestamp;
                timingEvent.FirstPlayerId = gameEvent.PlayerId;
                return true;
            }

            return false;
        }
    }
}
