using SimpleInjector;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class CommonDeps
    {
        public static void RegisterCommon(this Container container)
        {
            container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);
        }
    }
}
