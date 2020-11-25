using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface ILogTrimmer
    {
        IEnumerable<LogLine> Trim(IEnumerable<LogLine> lines, WorldInfo world);
    }
}
