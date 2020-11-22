using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingFinishAnalyzer : ITimingAnalyzer
    {
        private TimeSpan DragonKilledStrategyPenalty = TimeSpan.FromSeconds(15);
        private TimeSpan EnteredEndStrategyPenalty = TimeSpan.FromMinutes(5);
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromHours(2);

        private readonly ITimingAnalyzer _component;

        public TimingFinishAnalyzer(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<GameEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyse(RunInfo run)
        {
            var timings = _component.Analyse(run);

            if (!run.EndScreens.Any(x => x.Value))
            {
                timings.FinishTimeStrategy = FinishTimeStrategy.NotFinished;
                return timings;
            }

            var dragonEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Advancement
                         || x.Type == LogEventTypes.Achievement)
                .FirstOrDefault(x => x.Data == EventNames.FreeTheEnd);
            if (dragonEvent != null)
            {
                timings.FinishTimeStrategy = FinishTimeStrategy.DragonKilledStrategy;
                timings.FinishedOn = dragonEvent.Timestamp + DragonKilledStrategyPenalty;
                timings.RunTime = timings.FinishedOn.Value - timings.StartedOn;
                return timings;
            }

            var enteredEndEvent = ValidEvents
                .Where(x => x.Type == LogEventTypes.Advancement
                         || x.Type == LogEventTypes.Achievement)
                .FirstOrDefault(x => x.Data == EventNames.TheEnd);
            if (enteredEndEvent != null)
            {
                timings.FinishTimeStrategy = FinishTimeStrategy.EnteredEndStrategy;
                timings.FinishedOn = enteredEndEvent.Timestamp + EnteredEndStrategyPenalty;
                timings.RunTime = timings.FinishedOn.Value - timings.StartedOn;
                return timings;
            }

            timings.FinishTimeStrategy = FinishTimeStrategy.WildGuess;
            timings.FinishedOn = run.World.CreatedOn.ToLocalTime() + WorldCreationStrategyPenalty;
            timings.RunTime = timings.FinishedOn.Value - timings.StartedOn;
            return timings;
        }
    }
}
