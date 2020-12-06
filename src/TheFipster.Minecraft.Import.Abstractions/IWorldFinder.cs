using System.IO;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldFinder
    {
        FileSystemInfo Find(string worldname);
    }
}
