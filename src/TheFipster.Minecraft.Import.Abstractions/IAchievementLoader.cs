using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IAchievementLoader
    {
        Dictionary<string, ICollection<Achievement>> Load(WorldInfo world);
    }
}
