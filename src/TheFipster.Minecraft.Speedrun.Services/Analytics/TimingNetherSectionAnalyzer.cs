using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingNetherSectionAnalyzer : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;

        public TimingNetherSectionAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);
            var timingEvent = new TimingEvent(RunSections.Nether);

            if (!appendStartIfPossible(timingEvent))
                return timings;

            appendEndIfPossible(timingEvent);

            timings.Events.Add(timingEvent);
            return timings;
        }

        private bool appendStartIfPossible(TimingEvent timingEvent)
        {
            var weNeedToGoDeeperEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement
                         || x.Type == LogEventTypes.Advancement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.WeNeedToGoDeeper);

            if (weNeedToGoDeeperEvent != null)
            {
                timingEvent.Start = weNeedToGoDeeperEvent.Timestamp;
                timingEvent.FirstPlayerId = weNeedToGoDeeperEvent.Player.Id;
                return true;
            }

            return false;
        }

        private void appendEndIfPossible(TimingEvent timingEvent)
        {
            var blazePowderEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Achievement)
                .OrderBy(x => x.Timestamp)
                .FirstOrDefault(x => x.Data == EventNames.GotBlazePowder);

            if (blazePowderEvent != null)
            {
                timingEvent.End = blazePowderEvent.Timestamp;
                timingEvent.Time = timingEvent.End.Value - timingEvent.Start;
            }
        }
    }
}
