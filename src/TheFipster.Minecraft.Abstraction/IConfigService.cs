using System.IO;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IConfigService
    {
        DirectoryInfo ServerLocation { get; }
        DirectoryInfo LogLocation { get; }
        DirectoryInfo TempLocation { get; }
        DirectoryInfo DataLocation { get; }
        int InitialRunIndex { get; }
    }
}
