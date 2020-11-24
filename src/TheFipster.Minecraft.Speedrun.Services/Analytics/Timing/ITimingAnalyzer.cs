using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ITimingAnalyzer
    {
        TimingAnalytics Analyse(RunInfo run);
        IEnumerable<GameEvent> ValidEvents { get; }
    }
}
