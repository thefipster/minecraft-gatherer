using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services.World
{
    public class WorldDeleter : IWorldDeleter
    {
        private readonly DirectoryInfo serverFolder;

        public WorldDeleter(IConfigService config)
            => serverFolder = config.ServerLocation;

        public void Delete(string worldname)
        {
            var worldPath = Path.Combine(serverFolder.FullName, worldname);
            if (Directory.Exists(worldPath))
                Directory.Delete(worldPath, true);
        }
    }
}
