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

        public LogInterpreter(LogArchiveHandler archiveHandler)
        {
            this.archiveHandler = archiveHandler;
        }

        public IEnumerable<LogArchive> Split(string logArchivePath)
        {
            var log = archiveHandler.Read(logArchivePath);
            var splits = findSplits(log);

            if (splits.Count() == 0)
                yield break;

            var skipped = 0;
            foreach (var split in splits)
            {
                var take = split - skipped;
                if (take == 0) 
                    take = 1;

                var lines = log.Lines.Skip(skipped).Take(take);
                skipped = split;

                yield return new LogArchive
                {
                    Date = log.Date,
                    Lines = lines,
                    Name = log.Name,
                    Path = logArchivePath
                };
            }
        }

        private IEnumerable<int> findSplits(LogArchive log)
        {
            var lineCounter = -1;

            foreach (var line in log.Lines)
            {
                lineCounter++;

                if (line.Contains(LogStart))
                {
                    if (lineCounter == 0)
                        continue;

                    yield return lineCounter;
                }
            }

            yield return lineCounter;
        }
    }
}
