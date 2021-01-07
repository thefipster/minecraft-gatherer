using SimpleInjector;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;
using TheFipster.Minecraft.Meta.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class MetaDeps
    {
        public static void RegisterMeta(this Container container)
        {
            container.Register<IMetaDatabaseHandler, LiteMetaDatabaseHandler>(Lifestyle.Singleton);

            container.Register<IMapper<Sections, MetaFeatures>, FeatureSectionMapper>(Lifestyle.Singleton);

            container.Register<IRuntimeWriter, RuntimeWriter>(Lifestyle.Scoped);
            container.Register<IRuntimeFinder, RuntimeFinder>(Lifestyle.Scoped);

            container.Register<ITimingWriter, TimingWriter>(Lifestyle.Scoped);
            container.Register<ITimingFinder, TimingFinder>(Lifestyle.Scoped);
            container.RegisterDecorator<ITimingFinder, TimingBlacklistDecorator>(Lifestyle.Scoped);
            container.Register<ITimingBlacklister, TimingBlacklister>(Lifestyle.Scoped);

            container.Register<IOutcomeWriter, OutcomeWriter>(Lifestyle.Scoped);
            container.Register<IOutcomeFinder, OutcomeFinder>(Lifestyle.Scoped);

            container.RegisterDecorator<ITimingAnalyzer, RuntimeAnalyzerMetaDecorator>();
            container.RegisterDecorator<ITimingAnalyzer, TimingAnalyzerMetaDecorator>();
            container.RegisterDecorator<IOutcomeAnalyzer, OutcomeAnalyzerMetaDecorator>();
        }
    }
}
