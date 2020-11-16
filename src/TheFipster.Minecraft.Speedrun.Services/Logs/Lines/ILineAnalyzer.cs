using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILineAnalyzer
    {
        List<LogEvent> Analyze(LogLine line);
    }
}
