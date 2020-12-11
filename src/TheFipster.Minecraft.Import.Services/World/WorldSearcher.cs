using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class WorldSearcher : IWorldSearcher
    {
        private readonly IConfigService _config;

        public WorldSearcher(IConfigService config)
            => _config = config;

        public IEnumerable<DirectoryInfo> Find()
            => _config.ServerLocation.GetDirectories("world-*");
    }
}
