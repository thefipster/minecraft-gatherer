using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class OutcomeAnalyzer : IOutcomeAnalyzer
    {
        public Outcomes Analyze(RunImport run)
        {
            if (run.EndScreens.Any())
                return Outcomes.Finished;

            if (run.PlayerIds.Count() == 0)
                return Outcomes.Untouched;

            if (run.PlayerIds.Count() == 1)
                return Outcomes.Discarded;

            var validEvents = getValidEvents(run.Events);
            return outcomeFromEvents(validEvents);
        }

        private Outcomes outcomeFromEvents(IEnumerable<RunEvent> events)
        {
            if (events.Any(x => x.Value == EventNames.Advancements.FreeTheEnd))
                return Outcomes.Finished;

            if (events.Any(x => x.Value == EventNames.Advancements.TheEnd))
                return Outcomes.ResetEnd;

            if (events.Any(x => x.Value == EventNames.Advancements.EyeSpy))
                return Outcomes.ResetStronghold;

            if (events.Any(x => x.Value == EventNames.Advancements.GotEnderEye))
                return Outcomes.ResetSearch;

            if (events.Any(x => x.Value == EventNames.Advancements.ATerribleFortress))
                return Outcomes.ResetFortress;

            if (events.Any(x => x.Value == EventNames.Advancements.WeNeedToGoDeeper))
                return Outcomes.ResetNether;

            return Outcomes.ResetSpawn;
        }

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
