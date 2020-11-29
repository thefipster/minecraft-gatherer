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

            container.Register<IRunImportStore, RunImportLiteStore>(Lifestyle.Scoped);
            container.Register<IRunAnalyticsStore, RunAnalyticsLiteStore>(Lifestyle.Scoped);

            container.Register<IRunFinder, RunFinder>(Lifestyle.Scoped);
        }
    }
}
