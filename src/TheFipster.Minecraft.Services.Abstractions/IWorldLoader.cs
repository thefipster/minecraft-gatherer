using System.IO;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IWorldLoader
    {
        WorldInfo Load(DirectoryInfo worldFolder);
    }
}
