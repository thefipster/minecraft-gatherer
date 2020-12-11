using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldSearcher
    {
        IEnumerable<DirectoryInfo> Find();
    }
}
