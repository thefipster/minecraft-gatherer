using SimpleInjector;
using TheFipster.Minecraft.Import.Services;
using TheFipster.Minecraft.Modules;
using TheFipster.Minecraft.Services;
using TheFipster.Minecraft.Services.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public class ImportDeps
    {
        internal static void Register(Container container)
        {
            container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);

            container.Register<IDatabaseHandler, LiteDatabaseHandler>(Lifestyle.Singleton);
            container.Register<IRunImportStore, RunImportLiteStore>(Lifestyle.Transient);

            container.Register<IStatsFinder, StatsFinder>(Lifestyle.Transient);
            container.Register<IWorldFinder, WorldFinder>(Lifestyle.Transient);
            container.Register<ILogFinder, LogFinder>(Lifestyle.Transient);

            container.Register<IWorldLoader, WorldLoader>(Lifestyle.Transient);

            container.Register<IDimensionLoader, DimensionLoader>(Lifestyle.Transient);

            container.Register<ILogParser, LogParser>(Lifestyle.Transient);
            container.Register<ILogTrimmer, LogTrimmer>(Lifestyle.Transient);

            container.Register<IStatsExtractor, StatsExtractor>(Lifestyle.Transient);
            container.Register<IAchievementExtractor, AchievementExtractor>(Lifestyle.Transient);

            container.Register<IServerPropertiesReader, ServerPropertiesReader>(Lifestyle.Transient);

            container.Register<IPlayerNbtReader, PlayerNbtReader>(Lifestyle.Transient);

            container.Register<IWorldLoaderModule, WorldLoaderModule>(Lifestyle.Transient);
            container.Register<IImportRunModule, ImportRunModule>(Lifestyle.Transient);
            container.Register<ISyncModule, SyncModule>(Lifestyle.Transient);
        }
    }
}
