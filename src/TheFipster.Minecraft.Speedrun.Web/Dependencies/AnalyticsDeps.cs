using SimpleInjector;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Services;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Components;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public class AnalyticsDeps
    {
        internal static void Register(Container container)
        {
            container.Register<ITimingAnalyzer, TimingAnalyser>();
            container.RegisterDecorator<ITimingAnalyzer, TimingStartDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingFinishDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingSpawnSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingNetherSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingFortressSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingBlazeRodSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingSearchSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingStrongholdSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingTheEndSectionDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingPlaytimeDecorator>();

            container.Register<IOutcomeAnalyzer, OutcomeAnalyzer>();

            container.Register<IAnalyticsModule, AnalyticsModule>();
        }
    }
}
