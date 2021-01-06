using SimpleInjector;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Core.Services;
using TheFipster.Minecraft.Meta.Domain;
using TheFipster.Minecraft.Meta.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class CommonDeps
    {
        public static void RegisterCommon(this Container container)
        {
            container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);

            container.Register<IMapper<Sections, MetaFeatures>, FeatureSectionMapper>(Lifestyle.Singleton);

            container.Register<IMojangService, MojangService>();
            container.Register<IAuthService, AuthService>();
            container.Register<IWhitelistReader, WhitelistReader>();
            container.Register<IOpsReader, OpsReader>();
            container.Register<IHostEnv, HostEnv>();
        }
    }
}
