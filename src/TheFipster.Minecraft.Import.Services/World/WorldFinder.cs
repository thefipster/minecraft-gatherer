using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain.Exceptions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services.World
{
    public class WorldFinder : IWorldFinder
    {
        private const string ArchiveFoldername = "archive";

        private readonly DirectoryInfo serverFolder;
        private readonly DirectoryInfo archiveFolder;

        public WorldFinder(IConfigService config)
        {
            serverFolder = config.ServerLocation;

            var archivePath = Path.Combine(config.DataLocation.FullName, ArchiveFoldername);
            archiveFolder = new DirectoryInfo(archivePath);
            if (!archiveFolder.Exists)
                archiveFolder.Create();
        }

        public FileSystemInfo Find(string worldname)
        {
            var worldPath = Path.Combine(serverFolder.FullName, worldname);
            var worldFolder = new DirectoryInfo(worldPath);

            if (worldFolder.Exists)
                return worldFolder;

            var archivePath = Path.Combine(archiveFolder.FullName, $"{worldname}.zip");
            var archiveFile = new FileInfo(archivePath);
            if (archiveFile.Exists)
                return archiveFile;

            throw new WorldNotExistsException(worldname, new string[] { worldPath, archivePath });
        }
    }
}
