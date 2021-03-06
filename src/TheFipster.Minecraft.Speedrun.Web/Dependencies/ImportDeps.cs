﻿using SimpleInjector;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Services;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Services;
using TheFipster.Minecraft.Import.Services.World;
using TheFipster.Minecraft.Modules;
using TheFipster.Minecraft.Modules.Decorators;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class ImportDeps
    {
        public static void RegisterImporter(this Container container)
        {
            container.Register<IImportDatabaseHandler, LiteImportDatabaseHandler>(Lifestyle.Singleton);
            container.Register<IImportReader, ImportReader>(Lifestyle.Scoped);
            container.Register<IImportWriter, ImportWriter>(Lifestyle.Scoped);

            container.Register<IWorldSearcher, WorldSearcher>();
            container.Register<IWorldLoader, WorldLoader>();
            container.Register<IWorldFinder, WorldFinder>();
            container.Register<IWorldArchivist, WorldArchivist>();
            container.Register<IWorldDeleter, WorldDeleter>();

            container.Register<ILogFinder, LogFinder>();
            container.Register<ILogParser, LogParser>();
            container.Register<ILogTrimmer, LogTrimmer>();
            container.Register<ILogLoader, LogLoader>();

            container.Register<IStatsFinder, StatsFinder>();
            container.Register<IStatsLoader, StatsLoader>();

            container.Register<IAchievementLoader, AchievementLoader>();
            container.Register<IDimensionLoader, DimensionLoader>();
            container.Register<IServerPropertiesReader, ServerPropertiesReader>();

            container.Register<INbtEndScreenLoader, NbtEndScreenLoader>();
            container.Register<INbtLoader, NbtLoader>();
            container.RegisterDecorator<INbtLoader, NbtLevelDecorator>();
            container.RegisterDecorator<INbtLoader, NbtPlayerDecorator>();

            container.Register<IWorldLoaderModule, WorldLoaderModule>();
            container.Register<IImportModule, ImportModule>();
            container.RegisterDecorator<IImportModule, ImportArchiveDecorator>();
            container.Register<ISyncModule, SyncModule>();
        }
    }
}
