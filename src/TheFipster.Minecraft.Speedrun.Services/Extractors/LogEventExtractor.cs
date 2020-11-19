using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogEventExtractor : ILogEventExtractor
    {
        public IEnumerable<GameEvent> Extract(IEnumerable<LogLine> log)
            => new List<GameEvent>();
    }
}
