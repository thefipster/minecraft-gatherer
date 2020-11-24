using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingStartAnalyzer : ITimingAnalyzer
    {
        private TimeSpan PlayerJoinStrategyPenalty = TimeSpan.FromSeconds(1);
        private TimeSpan AchievementStrategyPenalty = TimeSpan.FromSeconds(30);
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromSeconds(20);

        private readonly ITimingAnalyzer _component;

        public TimingStartAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);

            var setTimeEvent = ValidEvents.FirstOrDefault(x => x.Type == LogEventTypes.SetTime);
            if (setTimeEvent != null)
            {
                timings.StartTimeStrategy = StartTimeStrategy.SetTimeStrategy;
                timings.StartedOn = setTimeEvent.Timestamp;
                return timings;
            }

            var playerJoinEvents = ValidEvents.Where(x => x.Type == LogEventTypes.Join);
            if (run.Players.Count() > 1 && playerJoinEvents.Count() > 1)
            {
                var secondJoinEvent = playerJoinEvents.OrderBy(x => x.Timestamp).Skip(1).First();
                timings.StartTimeStrategy = StartTimeStrategy.PlayerJoinStrategy;
                timings.StartedOn = secondJoinEvent.Timestamp - PlayerJoinStrategyPenalty;
                return timings;
            }

            var achievements = ValidEvents.Where(x => x.Type == LogEventTypes.Achievement);
            if (achievements.Any())
            {
                timings.StartTimeStrategy = StartTimeStrategy.AchievementsStrategy;
                timings.StartedOn = achievements.Min(x => x.Timestamp) - AchievementStrategyPenalty;
                return timings;
            }

            timings.StartTimeStrategy = StartTimeStrategy.WorldCreationStrategy;
            timings.StartedOn = run.World.CreatedOn.ToLocalTime() + WorldCreationStrategyPenalty;
            return timings;
        }
    }
}
