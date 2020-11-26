using SimpleInjector;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public class CommonDeps
    {
        internal static void Register(Container container)
        {
            container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);

            container.Register<IDatabaseHandler, LiteDatabaseHandler>(Lifestyle.Singleton);
        }
    }
}
