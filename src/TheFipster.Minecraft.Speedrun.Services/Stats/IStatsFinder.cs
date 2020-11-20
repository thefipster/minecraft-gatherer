using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IStatsFinder
    {
        IEnumerable<FileInfo> Find(string worldName);
    }
}
