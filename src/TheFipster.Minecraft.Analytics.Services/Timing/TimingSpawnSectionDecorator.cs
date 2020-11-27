using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingSpawnSectionDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingSpawnSectionDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunImport run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.Spawn);

            timingEvent.Start = timings.StartedOn;
            appendPlayerIfPossible(timingEvent);

            if (!appendEndIfPossible(timingEvent))
                return timings;

            timings.Events.Add(timingEvent);
            return timings;
        }

        private void appendPlayerIfPossible(TimingEvent timingEvent)
        {
            var firstJoinEvent = ValidEvents.Where(x => x.Type == EventTypes.Join).OrderBy(x => x.Timestamp).FirstOrDefault();
            if (firstJoinEvent != null)
                timingEvent.FirstPlayerId = firstJoinEvent.PlayerId;
        }

        private bool appendEndIfPossible(TimingEvent timingEvent)
        {
            var weNeedToGoDeeperEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.WeNeedToGoDeeper);

            if (weNeedToGoDeeperEvent != null)
            {
                timingEvent.End = weNeedToGoDeeperEvent.Timestamp;
                timingEvent.Time = timingEvent.End - timingEvent.Start;
                return true;
            }

            return false;
        }
    }
}
