using SimpleInjector;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Services;
using TheFipster.Minecraft.Modules;
using TheFipster.Minecraft.Core.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class ImportDeps
    {
        public static void RegisterImporter(this Container container)
        {
            container.Register<IStatsFinder, StatsFinder>();
            container.Register<IWorldFinder, WorldFinder>();
            container.Register<ILogFinder, LogFinder>();

            container.Register<IWorldLoader, WorldLoader>();

            container.Register<IDimensionLoader, DimensionLoader>();

            container.Register<ILogParser, LogParser>();
            container.Register<ILogTrimmer, LogTrimmer>();

            container.Register<IStatsExtractor, StatsExtractor>();
            container.Register<IAchievementExtractor, AchievementExtractor>();

            container.Register<IServerPropertiesReader, ServerPropertiesReader>();

            container.Register<IPlayerNbtReader, PlayerNbtReader>();

            container.Register<IWorldLoaderModule, WorldLoaderModule>();
            container.Register<IImportRunModule, ImportModule>();
            container.Register<ISyncModule, SyncModule>();
        }
    }
}
