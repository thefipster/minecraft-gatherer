using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain.Exceptions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services.World
{
    public class WorldFinder : IWorldFinder
    {
        private const string ArchiveFoldername = "archive";

        private readonly DirectoryInfo _serverFolder;
        private readonly DirectoryInfo _archiveFolder;
        private readonly DirectoryInfo _tempFolder;

        public WorldFinder(IConfigService config)
        {
            _serverFolder = config.ServerLocation;
            _tempFolder = config.TempLocation;

            var archivePath = Path.Combine(config.DataLocation.FullName, ArchiveFoldername);
            _archiveFolder = new DirectoryInfo(archivePath);
            if (!_archiveFolder.Exists)
                _archiveFolder.Create();
        }

        public FileSystemInfo Find(string worldname)
        {
            var worldPath = Path.Combine(_serverFolder.FullName, worldname);
            var worldFolder = new DirectoryInfo(worldPath);

            if (worldFolder.Exists)
                return worldFolder;

            var tempPath = Path.Combine(_tempFolder.FullName, ArchiveFoldername, worldname);
            var tempWorldFolder = new DirectoryInfo(tempPath);
            if (tempWorldFolder.Exists)
                return tempWorldFolder;

            var archivePath = Path.Combine(_archiveFolder.FullName, $"{worldname}.zip");
            var archiveFile = new FileInfo(archivePath);
            if (archiveFile.Exists)
                return archiveFile;

            throw new WorldNotExistsException(worldname, new string[] { worldPath, archivePath });
        }

        public Dictionary<Locations, FileSystemInfo> Locate(string worldname)
        {
            var locations = new Dictionary<Locations, FileSystemInfo>();

            var worldPath = Path.Combine(_serverFolder.FullName, worldname);
            var worldFolder = new DirectoryInfo(worldPath);
            if (worldFolder.Exists)
                locations.Add(Locations.Original, worldFolder);

            var archivePath = Path.Combine(_archiveFolder.FullName, $"{worldname}.zip");
            var archiveFile = new FileInfo(archivePath);
            if (archiveFile.Exists)
                locations.Add(Locations.Archive, archiveFile);

            var tempPath = Path.Combine(_tempFolder.FullName, ArchiveFoldername, worldname);
            var tempWorldFolder = new DirectoryInfo(tempPath);
            if (tempWorldFolder.Exists)
                locations.Add(Locations.Temp, tempWorldFolder);

            return locations;
        }
    }
}
