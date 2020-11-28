using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingBlazeRodSectionDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingBlazeRodSectionDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);
            var timingEvent = new TimingEvent(RunSections.BlazeRod);
            timingEvent.IsSubSplit = true;

            if (!appendStartIfPossible(timingEvent))
                return timings;

            if (!appendEndIfPossible(timingEvent))
                return timings;

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var terribleFortressEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.ATerribleFortress);

            if (terribleFortressEvent != null)
            {
                timingEvent.Start = terribleFortressEvent.Timestamp;
                timingEvent.FirstPlayerId = terribleFortressEvent.PlayerId;
                return true;
            }

            return false;
        }

        private bool appendEndIfPossible(TimingEvent timingEvent)
        {
            var intoFireEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.IntoFire);

            if (intoFireEvent != null)
            {
                timingEvent.End = intoFireEvent.Timestamp;
                timingEvent.Time = timingEvent.End - timingEvent.Start;
                return true;
            }

            return false;
        }
    }
}
