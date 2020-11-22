using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingSpawnSectionAnalyzer : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingSpawnSectionAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.Spawn);

            timingEvent.Start = timings.StartedOn;
            appendPlayerIfPossible(timingEvent);
            appendEndIfPossible(timingEvent);

            timings.Events.Add(timingEvent);
            return timings;
        }

        private void appendPlayerIfPossible(TimingEvent timingEvent)
        {
            var firstJoinEvent = ValidEvents.Where(x => x.Type == LogEventTypes.Join).OrderBy(x => x.Timestamp).FirstOrDefault();
            if (firstJoinEvent != null)
                timingEvent.FirstPlayerId = firstJoinEvent.Player.Id;
        }

        private void appendEndIfPossible(TimingEvent timingEvent)
        {
            var weNeedToGoDeeperEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.WeNeedToGoDeeper);

            if (weNeedToGoDeeperEvent != null)
            {
                timingEvent.End = weNeedToGoDeeperEvent.Timestamp;
                timingEvent.Time = timingEvent.End.Value - timingEvent.Start;
            }
        }
    }
}
