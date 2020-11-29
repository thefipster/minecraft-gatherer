using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldFinder
    {
        IEnumerable<DirectoryInfo> Find();
    }
}
