using SimpleInjector;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Services;
using TheFipster.Minecraft.Meta.Services.Lines;
using TheFipster.Minecraft.Meta.Services.Lines.Decorators;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public class MetaDeps
    {
        internal static void Register(Container container)
        {
            container.Register<IRunEventConverter, RunEventConverter>();

            container.Register<ILogLineEventConverter, LineEventConverter>();
            container.RegisterDecorator<ILogLineEventConverter, LineAdvancementDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineDeathDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineGameModeDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LinePlayerJoinDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LinePlayerLeaveDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineSetTimeDecorator>();
            container.RegisterDecorator<ILogLineEventConverter, LineTeleportDecorator>();
        }
    }
}
