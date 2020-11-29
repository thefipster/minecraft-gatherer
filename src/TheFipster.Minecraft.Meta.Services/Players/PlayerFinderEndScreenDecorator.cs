using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Players
{
    public class PlayerFinderEndScreenDecorator : IPlayerFinder
    {
        private readonly IPlayerFinder _component;

        public PlayerFinderEndScreenDecorator(IPlayerFinder component)
            => _component = component;

        public ICollection<string> Find(RunImport run)
        {
            var players = _component.Find(run);

            foreach (var id in run.EndScreens)
                if (!players.Contains(id))
                    players.Add(id);

            return players;
        }
    }
}
