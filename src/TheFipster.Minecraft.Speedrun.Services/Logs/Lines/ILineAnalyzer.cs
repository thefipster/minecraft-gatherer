using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILineAnalyzer
    {
        List<GameEvent> Analyze(LogLine line);
    }
}
