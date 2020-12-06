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
            container.Register<IRuntimeWriter, RuntimeWriter>(Lifestyle.Scoped);
            container.Register<ITimingWriter, TimingWriter>(Lifestyle.Scoped);
            container.Register<IOutcomeWriter, OutcomeWriter>(Lifestyle.Scoped);
            container.Register<IOutcomeFinder, OutcomeFinder>(Lifestyle.Scoped);

            container.RegisterDecorator<ITimingAnalyzer, RuntimeAnalyzerMetaDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingAnalyzerMetaDecorator>();
            container.RegisterDecorator<IOutcomeAnalyzer, OutcomeAnalyzerMetaDecorator>();
        }
    }
}
