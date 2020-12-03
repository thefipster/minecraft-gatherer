using SimpleInjector;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class MetaDeps
    {
        public static void RegisterMeta(this Container container)
        {
            container.Register<IRuntimeWriter, RuntimeWriter>();

            container.RegisterDecorator<ITimingAnalyzer, RuntimeMetaDecorator>();
        }
    }
}
