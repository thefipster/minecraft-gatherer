using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IAchievementExtractor
    {
        IEnumerable<GameEvent> Extract(WorldInfo world);
    }
}
