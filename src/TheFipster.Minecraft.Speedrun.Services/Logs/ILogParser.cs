using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILogParser
    {
        IEnumerable<LogLine> Read(IEnumerable<string> entries, DateTime date);
    }
}
