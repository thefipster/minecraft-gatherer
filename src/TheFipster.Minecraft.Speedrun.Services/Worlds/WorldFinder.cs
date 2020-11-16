using System.Collections.Generic;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
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
