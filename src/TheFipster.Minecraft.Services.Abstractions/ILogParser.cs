using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface ILogParser
    {
        IEnumerable<LogLine> Read(IEnumerable<string> entries, DateTime date);
    }
}
