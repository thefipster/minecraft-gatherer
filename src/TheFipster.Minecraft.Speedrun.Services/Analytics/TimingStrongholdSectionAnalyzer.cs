using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingStrongholdSectionAnalyzer : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingStrongholdSectionAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.Stronghold);

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
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.EyeSpy);

            if (gameEvent != null)
            {
                timingEvent.Start = gameEvent.Timestamp;
                timingEvent.FirstPlayerId = gameEvent.Player.Id;
                return true;
            }

            return false;
        }

        private bool appendEndIfPossible(TimingEvent timingEvent)
        {
            var gameEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.TheEnd);

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
