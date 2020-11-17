using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

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

            var content = archives.Select(x => decompress(x)).ToList();

            if (latest == null || latest.LastAccessTimeUtc.Date != date.Date)
                return content.SelectMany(x => x);

            var latestContent = File.ReadAllLines(latest.FullName);
            content.Add(latestContent);
            return content.SelectMany(x => x);
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

        private IEnumerable<string> decompress(FileInfo archive)
        {
            using (var archiveStream = archive.OpenRead())
            using (var memoryStream = new MemoryStream())
            using (var decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(memoryStream);
                var buffer = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(buffer, 0, buffer.Length);
                return getLines(buffer);
            }
        }

        private IEnumerable<string> getLines(byte[] buffer)
        {
            var logs = Encoding.UTF8.GetString(buffer);
            return logs.Split('\n');
        }
    }
}
