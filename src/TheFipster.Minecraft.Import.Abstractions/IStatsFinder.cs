using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IStatsFinder
    {
        IEnumerable<FileInfo> Find(WorldInfo world);
    }
}
