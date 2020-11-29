using SimpleInjector;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class ExtenderDeps
    {
        public static void RegisterExtender(this Container container)
        {
            container.Register<IPlayerEventEnhancer, PlayerEventEnhancer>();
            container.Register<IQuickestEventEnhancer, QuickestEventEnhancer>();
            container.Register<IRunCounterEnhancer, RunCounterEnhancer>();
        }
    }
}
