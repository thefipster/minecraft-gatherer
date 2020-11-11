using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IConfigService
    {
        DirectoryInfo ServerLocation { get; }
        DirectoryInfo LogLocation { get; }
        DirectoryInfo TempLocation { get; }
    }
}
