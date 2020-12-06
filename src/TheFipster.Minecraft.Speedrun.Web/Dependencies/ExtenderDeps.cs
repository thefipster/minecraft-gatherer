using SimpleInjector;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class ExtenderDeps
    {
        public static void RegisterExtender(this Container container)
        {
            container.Register<IPlayerEventExtender, PlayerEventExtender>();
            container.Register<IQuickestEventExtender, QuickestEventExtender>();
            container.Register<IRunCounterExtender, RunCounterExtender>();
            container.Register<IAttemptHeatmapExtender, AttemptHeatmapExtender>();
        }
    }
}
