using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IStatsExtractor
    {
        Dictionary<string, Stats> Extract(WorldInfo world);
    }
}
