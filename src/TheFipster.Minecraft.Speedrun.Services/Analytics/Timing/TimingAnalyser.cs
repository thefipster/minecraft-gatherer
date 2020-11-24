using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingAnalyser : ITimingAnalyzer
    {
        public TimingAnalytics Analyse(RunInfo run)
        {
            ValidEvents = getValidEvents(run.Events);
            return new TimingAnalytics(run.World.Name);
        }

        public IEnumerable<GameEvent> ValidEvents { get; private set; }

        private IEnumerable<GameEvent> getValidEvents(List<GameEvent> events)
        {
            if (events == null || events.Count() == 0)
                return Enumerable.Empty<GameEvent>();

            var cheatEvents = events
                .Where(e => e.Type == LogEventTypes.GameMode
                    || e.Type == LogEventTypes.Teleported);

            if (cheatEvents.Any())
                return events.Where(e => e.Timestamp < cheatEvents.Min(c => c.Timestamp));

            return events;
        }
    }
}
