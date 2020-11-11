using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogFinder : ILogFinder
    {
        private readonly IConfigService _config;

        public LogFinder(IConfigService config)
        {
            _config = config;

        }

        public IEnumerable<string> Find(DateTime date)
        {
            var archives = getArchives(date);
            var latest = getLatest(date);

            var contents = archives.Select(x => readArchive(x));

            // extract and read archives
            // parse log entries to get times
            // extract correct portion of logs for this world
            // date is the beginning of the speedrun, check the logs for that time to get the start, the end needs to be found based on the log start and message comparison.

            return contents.First();
        }

        private List<FileInfo> getArchives(DateTime date)
        {
            var allLogArchives = _config.LogLocation.GetFiles("*.log.gz");
            var candidates = new List<FileInfo>();
            foreach (var archive in allLogArchives)
            {
                var archiveDate = readDate(archive);
                if (archiveDate.Date == date.Date)
                    candidates.Add(archive);
            }

            return candidates;
        }

        private FileInfo getLatest(DateTime date)
        {
            var activeLog = _config.LogLocation.GetFiles("latest.log");
            if (activeLog.Any())
                return activeLog.First();

            return null;
        }

        private DateTime readDate(FileInfo archive)
        {
            var filename = archive.Name;

            var year = int.Parse(filename.Substring(0, 4));
            var month = int.Parse(filename.Substring(5, 2));
            var day = int.Parse(filename.Substring(8, 2));

            return new DateTime(year, month, day);
        }

        private IEnumerable<string> readArchive(FileInfo archive)
        {
            var contentFile = decompress(archive);
            var content = File.ReadAllLines(contentFile);
            File.Delete(contentFile);

            return content;
        }

        private string decompress(FileInfo archive)
        {
            var contentFile = Path.Combine(_config.TempLocation.FullName, $"{Guid.NewGuid()}.tmp");

            using (var archiveStream = archive.OpenRead())
            using (var contentStream = File.Create(contentFile))
            using (var decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
                decompressionStream.CopyTo(contentStream);

            return contentFile;
        }
    }
}
