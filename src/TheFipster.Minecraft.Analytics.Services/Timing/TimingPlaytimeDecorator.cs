using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;


namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingPlaytimeDecorator : ITimingAnalyzer
    {
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromHours(2);
        private TimeSpan EventsStrategyPenalty = TimeSpan.FromMinutes(10);

        private readonly ITimingAnalyzer _component;

        public TimingPlaytimeDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunImport run)
        {
            var timings = _component.Analyse(run);

            if (run.PlayerIds.Count() == 0 || run.Dimensions.Count() == 0)
                return timings;

            var start = getPlaytimeStart(timings, run);
            if (start > timings.StartedOn)
                start = timings.StartedOn;

            var end = getPlaytimeEnd(timings, run);
            if (timings.FinishedOn.HasValue && timings.FinishedOn > end)
                end = timings.FinishedOn.Value;

            timings.PlayTime = end - start;

            return timings;
        }

        private DateTime getPlaytimeStart(TimingAnalytics timings, RunImport run)
        {
            var firstJoinEvent = run.Events.Where(x => x.Type == EventTypes.Join).OrderBy(x => x.Timestamp).FirstOrDefault();
            if (firstJoinEvent != null)
                return firstJoinEvent.Timestamp;

            if (run.Events.Any())
                return run.Events.Min(x => x.Timestamp);

            return timings.StartedOn;
        }

        private DateTime getPlaytimeEnd(TimingAnalytics timings, RunImport run)
        {
            var lastLeaveEvent = run.Events.Where(x => x.Type == EventTypes.Leave).OrderByDescending(x => x.Timestamp).FirstOrDefault();
            if (lastLeaveEvent != null)
                return lastLeaveEvent.Timestamp;

            if (run.Events.Any())
                return run.Events.Max(x => x.Timestamp) + EventsStrategyPenalty;

            return run.World.CreatedOn + WorldCreationStrategyPenalty;
        }
    }
}
