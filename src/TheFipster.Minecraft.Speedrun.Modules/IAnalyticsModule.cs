using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public interface IAnalyticsModule
    {
        RunAnalytics Analyze(RunInfo run);

    }
}
