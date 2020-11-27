using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Players
{
    public class PlayerFinderStatsDecorator : IPlayerFinder
    {
        private readonly IPlayerFinder _component;

        public PlayerFinderStatsDecorator(IPlayerFinder component)
            => _component = component;

        public ICollection<string> Find(RunImport run)
        {
            var players = _component.Find(run);

            foreach (var id in run.Stats.Select(x => x.Key))
                if (!players.Contains(id))
                    players.Add(id);

            return players;
        }
    }
}
