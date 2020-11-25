using System.IO;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldLoader
    {
        WorldInfo Load(DirectoryInfo worldFolder);
    }
}
