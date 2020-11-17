using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IWorldFinder
    {
        IEnumerable<DirectoryInfo> Find();
    }
}
