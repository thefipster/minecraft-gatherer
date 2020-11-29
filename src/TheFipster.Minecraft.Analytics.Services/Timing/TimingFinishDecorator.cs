using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingFinishDecorator : ITimingAnalyzer
    {
        private TimeSpan DragonKilledStrategyPenalty = TimeSpan.FromSeconds(15);
        private TimeSpan EnteredEndStrategyPenalty = TimeSpan.FromMinutes(5);
        private TimeSpan WorldCreationStrategyPenalty = TimeSpan.FromHours(2);

        private readonly ITimingAnalyzer _component;

        public TimingFinishDecorator(ITimingAnalyzer component)
            => _component = component;

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);

            if (run.EndScreens.Count() == 0)
            {
                timings.FinishTimeStrategy = FinishTimeStrategy.NotFinished;
                return timings;
            }

            var dragonEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.FreeTheEnd);
            if (dragonEvent != null)
            {
                timings.FinishTimeStrategy = FinishTimeStrategy.DragonKilledStrategy;
                timings.FinishedOn = dragonEvent.Timestamp + DragonKilledStrategyPenalty;
                timings.RunTime = timings.FinishedOn.Value - timings.StartedOn;
                return timings;
            }

            var enteredEndEvent = ValidEvents
                .Where(x => x.Type == EventTypes.Advancement)
                .FirstOrDefault(x => x.Value == EventNames.Advancements.TheEnd);
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
