using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingBlazeRodSectionAnalyzer : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingBlazeRodSectionAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.BlazeRod);
            timingEvent.IsSubSplit = true;

            if (!appendStartIfPossible(timingEvent))
                return timings;

            appendEndIfPossible(timingEvent);

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var terribleFortressEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.ATerribleFortress);

            if (terribleFortressEvent != null)
            {
                timingEvent.Start = terribleFortressEvent.Timestamp;
                timingEvent.FirstPlayerId = terribleFortressEvent.Player.Id;
                return true;
            }

            return false;
        }

        private void appendEndIfPossible(TimingEvent timingEvent)
        {
            var intoFireEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.IntoFire);

            if (intoFireEvent != null)
            {
                timingEvent.End = intoFireEvent.Timestamp;
                timingEvent.Time = timingEvent.End.Value - timingEvent.Start;
            }
        }
    }
}
