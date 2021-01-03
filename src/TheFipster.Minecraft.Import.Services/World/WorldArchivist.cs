using System;
using System.IO;
using System.IO.Compression;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain.Exceptions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services.World
{
    public class WorldArchivist : IWorldArchivist
    {
        private const string ArchiveFoldername = "archive";

        private readonly DirectoryInfo _serverFolder;
        private readonly DirectoryInfo _tempFolder;
        private readonly DirectoryInfo _archiveFolder;

        private readonly IWorldDeleter _deleter;

        public WorldArchivist(IConfigService config, IWorldDeleter deleter, IHostEnv hostEnv)
        {
            _deleter = deleter;
            _serverFolder = config.ServerLocation;
            _tempFolder = config.TempLocation;

            var archivePath = Path.Combine(config.DataLocation.FullName, ArchiveFoldername);
            _archiveFolder = new DirectoryInfo(archivePath);
            if (!_archiveFolder.Exists)
                _archiveFolder.Create();
        }

        public FileInfo Compress(string worldname)
        {
            var worldPath = Path.Combine(_serverFolder.FullName, worldname);

            if (!Directory.Exists(worldPath))
                throw new WorldNotExistsException(worldname, worldPath);

            var archivePath = Path.Combine(_archiveFolder.FullName, $"{worldname}.zip");
            if (File.Exists(archivePath))
                File.Delete(archivePath);

            ZipFile.CreateFromDirectory(worldPath, archivePath);
            if (!File.Exists(archivePath))
                throw new Exception("Compression faild.");

            return new FileInfo(archivePath);
        }

        public DirectoryInfo Decompress(string worldname)
        {
            var archivePath = Path.Combine(_archiveFolder.FullName, $"{worldname}.zip");
            if (!File.Exists(archivePath))
                throw new WorldNotExistsException(worldname, archivePath);

            var worldPath = Path.Combine(_tempFolder.FullName, ArchiveFoldername, worldname);
            _deleter.Delete(worldname);

            ZipFile.ExtractToDirectory(archivePath, worldPath);
            if (!Directory.Exists(worldPath))
                throw new Exception("Decompression faild.");

            return new DirectoryInfo(worldPath);
        }
    }
}
