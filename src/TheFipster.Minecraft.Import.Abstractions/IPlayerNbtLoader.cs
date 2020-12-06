using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface INbtEndScreenLoader
    {
        ICollection<string> Load(WorldInfo world);
    }
}
