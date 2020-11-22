using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingPlaytimeAnalyzer : ITimingAnalyzer
    {
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromHours(2);
        private TimeSpan EventsStrategyPenalty = TimeSpan.FromMinutes(10);

        private readonly ITimingAnalyzer _component;

        public TimingPlaytimeAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);

            var start = getPlaytimeStart(timings, run);
            if (start > timings.StartedOn)
                start = timings.StartedOn;

            var end = getPlaytimeEnd(timings, run);
            if (timings.FinishedOn.HasValue && timings.FinishedOn > end)
                end = timings.FinishedOn.Value;

            timings.PlayTime = end - start;

            return timings;
        }

        private DateTime getPlaytimeStart(TimingAnalytics timings, RunInfo run)
        {
            var firstJoinEvent = run.Events.Where(x => x.Type == LogEventTypes.Join).OrderBy(x => x.Timestamp).FirstOrDefault();
            if (firstJoinEvent != null)
                return firstJoinEvent.Timestamp;

            if (run.Events.Any())
                return run.Events.Min(x => x.Timestamp);

            return timings.StartedOn;
        }

        private DateTime getPlaytimeEnd(TimingAnalytics timings, RunInfo run)
        {
            var lastLeaveEvent = run.Events.Where(x => x.Type == LogEventTypes.Leave).OrderByDescending(x => x.Timestamp).FirstOrDefault();
            if (lastLeaveEvent != null)
                return lastLeaveEvent.Timestamp;

            if (run.Events.Any())
                return run.Events.Max(x => x.Timestamp) + EventsStrategyPenalty;

            return run.World.CreatedOn + WorldCreationStrategyPenalty;
        }
    }
}
