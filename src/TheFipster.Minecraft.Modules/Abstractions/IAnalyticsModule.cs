using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules.Abstractions
{
    public interface IAnalyticsModule
    {
        RunAnalytics Analyze(RunImport run);
    }
}
