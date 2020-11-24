using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Services.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class WorldFinder : IWorldFinder
    {
        private readonly IConfigService _config;

        public WorldFinder(IConfigService config)
            => _config = config;

        public IEnumerable<DirectoryInfo> Find()
            => _config.ServerLocation.GetDirectories("world-*");
    }
}
