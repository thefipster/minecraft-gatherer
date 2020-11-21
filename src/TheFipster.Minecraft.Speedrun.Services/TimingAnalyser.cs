using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingAnalyser
    {
        private TimeSpan PlayerJoinStrategyPenalty = TimeSpan.FromSeconds(1);
        private TimeSpan AchievementStrategyPenalty = TimeSpan.FromSeconds(30);
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromSeconds(20);

        private RunInfo _run;

        public TimingAnalytics Analyse(RunInfo run)
        {
            _run = run;
            var timings = new TimingAnalytics(run.World.Name);

            if (run.Events == null || run.Events.Count() == 0)
                return timings;

            var events = getValidEvents(run.Events);

            appendStartTime(timings, events);
            appendSpawnSection(timings, events);

            return timings;
        }

        private void appendStartTime(TimingAnalytics timings, IEnumerable<GameEvent> events)
        {
            var setTimeEvent = events.FirstOrDefault(x => x.Type == LogEventTypes.SetTime);
            if (setTimeEvent != null)
            {
                timings.StartTimeStrategy = StartTimeStrategy.SetTimeStrategy;
                timings.StartedOn = setTimeEvent.Timestamp;
                return;
            }

            var playerJoinEvents = events.Where(x => x.Type == LogEventTypes.Join);
            if (playerJoinEvents.Count() > 1)
            {
                var secondJoinEvent = playerJoinEvents.OrderBy(x => x.Timestamp).Skip(1).First();
                timings.StartTimeStrategy = StartTimeStrategy.PlayerJoinStrategy;
                timings.StartedOn = secondJoinEvent.Timestamp - PlayerJoinStrategyPenalty;
                return;
            }

            var achievements = events.Where(x => x.Type == LogEventTypes.Achievement);
            if (achievements.Any())
            {
                timings.StartTimeStrategy = StartTimeStrategy.AchievementsStrategy;
                timings.StartedOn = achievements.Min(x => x.Timestamp) - AchievementStrategyPenalty;
                return;
            }

            timings.StartTimeStrategy = StartTimeStrategy.WorldCreationStrategy;
            timings.StartedOn = _run.World.CreatedOn.ToLocalTime() + WorldCreationStrategyPenalty;
        }

        private void appendSpawnSection(TimingAnalytics timings, IEnumerable<GameEvent> events)
        {
            var timing = new TimingEvent(RunSections.Spawn);



        }

        private IEnumerable<GameEvent> getValidEvents(List<GameEvent> events)
        {
            var cheatEvents = events.Where(e => e.Type == LogEventTypes.GameMode || e.Type == LogEventTypes.Teleported);
            if (cheatEvents.Any())
                return events.Where(e => e.Timestamp < cheatEvents.Min(c => c.Timestamp));

            return events;
        }
    }
}
