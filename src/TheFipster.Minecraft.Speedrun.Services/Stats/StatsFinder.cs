using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class StatsFinder : IStatsFinder
    {
        private const string StatsFoldername = "stats";

        private readonly IConfigService _config;

        public StatsFinder(IConfigService config)
            => _config = config;

        public IEnumerable<FileInfo> Find(string worldName)
        {
            var statsPath = Path.Combine(_config.ServerLocation.FullName, worldName, StatsFoldername);
            var statsFolder = new DirectoryInfo(statsPath);

            if (!statsFolder.Exists)
                return Enumerable.Empty<FileInfo>();

            return statsFolder.GetFiles("*.json");
        }
    }
}
