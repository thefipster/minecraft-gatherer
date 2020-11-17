using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IWorldLoader
    {
        WorldInfo Load(DirectoryInfo worldFolder);
    }
}
