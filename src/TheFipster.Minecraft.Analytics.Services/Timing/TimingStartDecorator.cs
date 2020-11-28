using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingStartDecorator : ITimingAnalyzer
    {
        private TimeSpan PlayerJoinStrategyPenalty = TimeSpan.FromSeconds(1);
        private TimeSpan AchievementStrategyPenalty = TimeSpan.FromSeconds(30);
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromSeconds(20);

        private readonly ITimingAnalyzer _component;

        public TimingStartDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);

            var setTimeEvent = ValidEvents.FirstOrDefault(x => x.Type == EventTypes.SetTime);
            if (setTimeEvent != null)
            {
                timings.StartTimeStrategy = StartTimeStrategy.SetTimeStrategy;
                timings.StartedOn = setTimeEvent.Timestamp;
                return timings;
            }

            var playerJoinEvents = ValidEvents.Where(x => x.Type == EventTypes.Join);
            if (run.PlayerIds.Count() > 1 && playerJoinEvents.Count() > 1)
            {
                var secondJoinEvent = playerJoinEvents.OrderBy(x => x.Timestamp).Skip(1).First();
                timings.StartTimeStrategy = StartTimeStrategy.PlayerJoinStrategy;
                timings.StartedOn = secondJoinEvent.Timestamp - PlayerJoinStrategyPenalty;
                return timings;
            }

            var achievements = ValidEvents.Where(x => x.Type == EventTypes.Advancement);
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
