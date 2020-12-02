using System.IO;

namespace TheFipster.Minecraft.Core.Abstractions
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
