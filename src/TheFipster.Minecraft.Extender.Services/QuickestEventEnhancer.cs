using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class QuickestEventEnhancer : IQuickestEventEnhancer
    {
        public IEnumerable<FirstEvent> Enhance(RunImport run)
        {
            if (run.Logs == null)
                return Enumerable.Empty<FirstEvent>();

            var firstEverything = new List<FirstEvent>();
            var advancements = run.Events.Where(x => x.Type == EventTypes.Advancement).Select(x => x.Value).Distinct();

            foreach (var advancement in advancements)
            {
                var fastest = run.Events
                    .Where(x => (x.Type == EventTypes.Advancement)
                             && x.Value == advancement)
                    .OrderBy(x => x.Timestamp)
                    .First();

                var first = new FirstEvent
                {
                    Name = advancement,
                    PlayerId = fastest.PlayerId,
                    Time = fastest.Timestamp
                };

                firstEverything.Add(first);
            }

            return firstEverything;
        }
    }
}
