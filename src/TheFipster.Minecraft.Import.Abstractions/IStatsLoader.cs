using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IStatsLoader
    {
        Dictionary<string, Stats> Load(WorldInfo world);
    }
}
