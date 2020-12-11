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

        private readonly DirectoryInfo serverFolder;
        private readonly DirectoryInfo archiveFolder;

        private readonly IWorldDeleter _deleter;

        public WorldArchivist(IConfigService config, IWorldDeleter deleter)
        {
            _deleter = deleter;

            serverFolder = config.ServerLocation;

            var archivePath = Path.Combine(config.DataLocation.FullName, ArchiveFoldername);
            archiveFolder = new DirectoryInfo(archivePath);
            if (!archiveFolder.Exists)
                archiveFolder.Create();
        }

        public FileInfo Compress(string worldname)
        {
            var worldPath = Path.Combine(serverFolder.FullName, worldname);

            if (!Directory.Exists(worldPath))
                throw new WorldNotExistsException(worldname, worldPath);

            var archivePath = Path.Combine(archiveFolder.FullName, $"{worldname}.zip");
            if (File.Exists(archivePath))
                File.Delete(archivePath);

            ZipFile.CreateFromDirectory(worldPath, archivePath);
            if (!File.Exists(archivePath))
                throw new Exception("Compression faild.");

            return new FileInfo(archivePath);
        }

        public DirectoryInfo Decompress(string worldname)
        {
            var archivePath = Path.Combine(archiveFolder.FullName, $"{worldname}.zip");
            if (!File.Exists(archivePath))
                throw new WorldNotExistsException(worldname, archivePath);

            var worldPath = Path.Combine(serverFolder.FullName, worldname);
            _deleter.Delete(worldname);

            ZipFile.ExtractToDirectory(archivePath, worldPath);
            if (!Directory.Exists(worldPath))
                throw new Exception("Decompression faild.");

            return new DirectoryInfo(worldPath);
        }
    }
}
