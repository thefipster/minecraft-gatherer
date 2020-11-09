using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

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

        public IEnumerable<string> Read(string logArchivePath)
        {
            var contentPath = decompress(logArchivePath);
            var log = read(contentPath);
            delete(contentPath);

            return log;
        }

        private string decompress(string logArchivePath)
        {
            var logArchive = new FileInfo(logArchivePath);
            var contentFile = Path.Combine(config.TempPath, $"{Guid.NewGuid()}.tmp");

            using (FileStream archiveStream = logArchive.OpenRead())
            using (FileStream contentStream = File.Create(contentFile))
            using (GZipStream decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
                decompressionStream.CopyTo(contentStream);

            return contentFile;
        }

        private IEnumerable<string> read(string contentPath) => File.ReadAllLines(contentPath);

        private void delete(string path)
        {
            if (!path.Contains(config.TempPath))
                throw new ApplicationException($"Not allowed to delete files outside of temp. Tried to delete '{path}'");

            File.Delete(path);
        }
    }
}
