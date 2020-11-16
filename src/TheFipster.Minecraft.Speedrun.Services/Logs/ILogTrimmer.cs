using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILogTrimmer
    {
        IEnumerable<LogLine> Trim(IEnumerable<LogLine> lines, WorldInfo world);
    }
}
