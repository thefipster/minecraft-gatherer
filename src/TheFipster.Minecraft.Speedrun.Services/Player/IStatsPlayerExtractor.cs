using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IStatsPlayerExtractor
    {
        IEnumerable<Player> Extract(IEnumerable<PlayerStats> stats);
    }
}
