using SimpleInjector;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Services;
using TheFipster.Minecraft.Analytics.Services.Players;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Components;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class AnalyticsDeps
    {
        public static void RegisterAnalytics(this Container container)
        {
            container.Register<IAnalyticsDatabaseHandler, LiteAnalyticsDatabaseHandler>(Lifestyle.Singleton);

            container.Register<IAnalyticsReader, AnalyticsReader>(Lifestyle.Scoped);
            container.Register<IAnalyticsWriter, AnalyticsWriter>(Lifestyle.Scoped);
            container.Register<IRunIndexer, RunIndexer>(Lifestyle.Scoped);

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

            container.Register<IPlayerAnalyzer, PlayerAnalyzer>();
            container.RegisterDecorator<IPlayerAnalyzer, PlayerStatisticsCounterDecorator>();

            container.Register<IAnalyticsModule, AnalyticsModule>();
        }
    }
}
