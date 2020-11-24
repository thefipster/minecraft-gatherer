using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IWorldFinder
    {
        IEnumerable<DirectoryInfo> Find();
    }
}
