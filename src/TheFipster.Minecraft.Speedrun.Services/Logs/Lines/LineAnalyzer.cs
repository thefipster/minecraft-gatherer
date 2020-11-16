using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineAnalyzer : ILineAnalyzer
    {
        public List<LogEvent> Analyze(LogLine line)
        {
            return new List<LogEvent>();
        }
    }
}
