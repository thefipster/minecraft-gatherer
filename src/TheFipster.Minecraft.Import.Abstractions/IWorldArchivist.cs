using System.IO;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldArchivist
    {
        FileInfo Compress(string worldname);
        DirectoryInfo Decompress(string worldname);
    }
}
