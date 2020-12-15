using SimpleInjector;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class CommonDeps
    {
        public static void RegisterCommon(this Container container)
        {
            container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);
            container.Register<IMojangService, MojangService>();
            container.Register<IAuthService, AuthService>();
            container.Register<IWhitelistReader, WhitelistReader>();
            container.Register<IOpsReader, OpsReader>();
        }
    }
}
