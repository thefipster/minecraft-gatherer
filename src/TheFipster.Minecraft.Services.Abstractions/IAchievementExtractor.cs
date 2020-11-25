using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IAchievementExtractor
    {
        Dictionary<string, ICollection<Achievement>> Extract(WorldInfo world);
    }
}
