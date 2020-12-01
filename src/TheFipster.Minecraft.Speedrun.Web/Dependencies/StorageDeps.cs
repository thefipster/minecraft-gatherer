using SimpleInjector;
using TheFipster.Minecraft.Storage.Abstractions;
using TheFipster.Minecraft.Storage.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class StorageDeps
    {
        public static void RegisterStorage(this Container container)
        {
            container.Register<IDatabaseHandler, LiteDatabaseHandler>(Lifestyle.Singleton);

            container.Register<IImportStore, ImportLiteStore>(Lifestyle.Scoped);
            container.Register<IAnalyticsStore, AnalyticsLiteStore>(Lifestyle.Scoped);
            container.Register<IManualsStore, ManualsLiteStore>(Lifestyle.Scoped);

            container.Register<IRunFinder, RunFinder>(Lifestyle.Scoped);
        }
    }
}
