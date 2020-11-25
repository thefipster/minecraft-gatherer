using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IDimensionLoader
    {
        ICollection<DimensionInfo> Load(WorldInfo world);
    }
}
