using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILogEventExtractor
    {
        IEnumerable<GameEvent> Extract(IEnumerable<LogLine> log);
    }
}
