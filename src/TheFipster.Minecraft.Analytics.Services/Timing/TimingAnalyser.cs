using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingAnalyser : ITimingAnalyzer
    {
        public TimingAnalytics Analyse(RunImport run)
        {
            ValidEvents = getValidEvents(run.Events);
            return new TimingAnalytics(run.World.Name);
        }

        public IEnumerable<RunEvent> ValidEvents { get; private set; }

        private IEnumerable<RunEvent> getValidEvents(ICollection<RunEvent> events)
        {
            if (events == null || events.Count() == 0)
                return Enumerable.Empty<RunEvent>();

            var cheatEvents = events
                .Where(e => e.Type == EventTypes.GameMode
                    || e.Type == EventTypes.Teleport);

            if (cheatEvents.Any())
                return events.Where(e => e.Timestamp < cheatEvents.Min(c => c.Timestamp));

            return events;
        }
    }
}
