using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LogInterpreter
    {
        private const string LogStart = @"Environment: authHost='https://authserver.mojang.com', accountsHost='https://api.mojang.com', sessionHost='https://sessionserver.mojang.com', name='PROD'";

        private readonly LogArchiveHandler archiveHandler;
        private readonly LineInterpreter lineInterpreter;

        public LogInterpreter(LogArchiveHandler archiveHandler, LineInterpreter lineInterpreter)
        {
            this.archiveHandler = archiveHandler;
            this.lineInterpreter = lineInterpreter;
        }

        public IEnumerable<LogSession> Split(string logArchivePath)
        {
            var file = new FileInfo(logArchivePath);
            var date = readDateFromFile(file.Name);
            var log = archiveHandler.Read(logArchivePath);
            var splits = findSplits(log);

            if (splits.Count() == 0)
                yield break;

            var skipped = 0;
            foreach (var split in splits)
            {
                var lines = log.Skip(skipped).Take(split - skipped);
                skipped = split;

                lineInterpreter.Read(lines.First(), date);

                yield return new LogSession
                {
                    OriginalArchive = logArchivePath
                };
            }
        }

        private DateTime readDateFromFile(string logArchivePath)
        {
            var year = int.Parse(logArchivePath.Substring(0, 4));
            var month = int.Parse(logArchivePath.Substring(5, 2));
            var day = int.Parse(logArchivePath.Substring(8, 2));

            return new DateTime(year, month, day);
        }

        private IEnumerable<int> findSplits(IEnumerable<string> lines)
        {
            int lineCounter = -1;
            int lastSplit = 0;
            foreach (var line in lines)
            {
                lineCounter++;

                if (line.Contains(LogStart))
                {
                    if (lineCounter == lastSplit)
                        continue;

                    yield return lineCounter;
                }
            }

            yield return lineCounter;
        }
    }
}
