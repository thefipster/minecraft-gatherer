using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IStatsExtractor
    {
        IEnumerable<PlayerStats> Extract(string worldName);
    }
}
