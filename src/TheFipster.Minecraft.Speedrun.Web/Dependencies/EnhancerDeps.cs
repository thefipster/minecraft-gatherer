using SimpleInjector;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Services;
using TheFipster.Minecraft.Enhancer.Services.Decorators;
using TheFipster.Minecraft.Enhancer.Services.Lines;
using TheFipster.Minecraft.Enhancer.Services.Lines.Decorators;
using TheFipster.Minecraft.Enhancer.Services.Players;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Components;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public class EnhancerDeps
    {
        internal static void Register(Container container)
        {
            container.Register<IRunEventConverter, RunEventConverter>();
            container.RegisterDecorator<IRunEventConverter, RunEventLogDecorator>();
            container.RegisterDecorator<IRunEventConverter, RunEventAdvancementDecorator>();

            container.Register<ILogLineEventConverter, LineEventConverter>();
            container.RegisterDecorator<ILogLineEventConverter, LineAdvancementDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineDeathDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineGameModeDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LinePlayerJoinDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LinePlayerLeaveDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineSetTimeDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineTeleportDecorator>();

            container.Register<IPlayerFinder, PlayerFinder>();
            container.RegisterDecorator<IPlayerFinder, PlayerFinderAchievementDecorator>();
            container.RegisterDecorator<IPlayerFinder, PlayerFinderEndScreenDecorator>();
            container.RegisterDecorator<IPlayerFinder, PlayerFinderStatsDecorator>();

            container.Register<IEnhanceModule, EnhanceModule>();
        }
    }
}
