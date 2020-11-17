using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LogArchiveHandler
    {
        private readonly FilesystemConfig config;

        public LogArchiveHandler(FilesystemConfig config)
        {
            this.config = config;
        }

        public IEnumerable<string> Find() => Directory.GetFiles(config.LogPath, "*.log.gz");

        public IEnumerable<string> Find(DateTime date) => Directory.GetFiles(config.LogPath, $"{date:YYYY-MM-dd}-*.log.gz");

        public LogArchive Read(string logArchivePath)
        {
            var filename = readFilename(logArchivePath);
            var date = readDate(logArchivePath);
            var log = readLog(logArchivePath);

            return new LogArchive
            {
                Date = date,
                Lines = log,
                Name = filename,
                Path = logArchivePath
            };
        }

        private string readFilename(string logArchivePath)
        {
            var fileInfo = new FileInfo(logArchivePath);
            return fileInfo.Name;
        }

        private DateTime readDate(string logArchivePath)
        {
            var filename = readFilename(logArchivePath);

            var year = int.Parse(filename.Substring(0, 4));
            var month = int.Parse(filename.Substring(5, 2));
            var day = int.Parse(filename.Substring(8, 2));

            return new DateTime(year, month, day);
        }

        private IEnumerable<string> readLog(string logArchivePath)
        {
            var contentPath = decompressToTemp(logArchivePath);
            var log = readLines(contentPath);
            deleteTemp(contentPath);
            return log;
        }

        private string decompressToTemp(string logArchivePath)
        {
            var logArchive = new FileInfo(logArchivePath);
            var contentFile = Path.Combine(config.TempPath, $"{Guid.NewGuid()}.tmp");

            using (var archiveStream = logArchive.OpenRead())
            using (var contentStream = File.Create(contentFile))
            using (var decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
                decompressionStream.CopyTo(contentStream);

            return contentFile;
        }

        private IEnumerable<string> readLines(string contentPath) => File.ReadAllLines(contentPath);

        private void deleteTemp(string path)
        {
            if (!path.Contains(config.TempPath))
                throw new ApplicationException($"Not allowed to delete files outside of temp. Tried to delete '{path}'");

            File.Delete(path);
        }
    }
}
