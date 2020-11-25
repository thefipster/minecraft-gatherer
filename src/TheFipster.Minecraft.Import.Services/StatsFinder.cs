using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class StatsFinder : IStatsFinder
    {
        private const string StatsFoldername = "stats";

        public IEnumerable<FileInfo> Find(WorldInfo world)
        {
            var statsPath = Path.Combine(world.Path, StatsFoldername);
            var statsFolder = new DirectoryInfo(statsPath);

            if (!statsFolder.Exists)
                return Enumerable.Empty<FileInfo>();

            return statsFolder.GetFiles("*.json");
        }
    }
}
