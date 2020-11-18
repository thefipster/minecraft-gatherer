using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Web.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public class FirstToAdvancementEnhancer : IFirstToAdvancementEnhancer
    {
        public IEnumerable<FirstEvent> Enhance(RunInfo run)
        {
            if (run.Logs == null)
                return Enumerable.Empty<FirstEvent>();

            var firstEverything = new List<FirstEvent>();
            var advancements = run.Logs.Events.Where(x => x.Type == LogEventTypes.Advancement).Select(x => x.Data).Distinct();

            foreach (var advancement in advancements)
            {
                var fastest = run.Logs.Events
                    .Where(x => x.Type == LogEventTypes.Advancement
                             && x.Data == advancement)
                    .OrderBy(x => x.Timestamp)
                    .First();

                var first = new FirstEvent
                {
                    Name = advancement,
                    Player = fastest.Player,
                    Time = fastest.Timestamp
                };

                firstEverything.Add(first);
            }

            return firstEverything;
        }
    }
}
