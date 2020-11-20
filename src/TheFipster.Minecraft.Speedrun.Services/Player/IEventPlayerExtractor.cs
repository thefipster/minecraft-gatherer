using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IEventPlayerExtractor
    {
        IEnumerable<Player> Extract(IEnumerable<GameEvent> events);
    }
}
