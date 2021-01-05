using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class OutcomeAnalyzerMetaDecorator : IOutcomeAnalyzer
    {
        private readonly IOutcomeAnalyzer _component;
        private readonly IOutcomeWriter _writer;

        public OutcomeAnalyzerMetaDecorator(IOutcomeAnalyzer component, IOutcomeWriter writer)
        {
            _component = component;
            _writer = writer;
        }

        public Outcomes Analyze(RunImport run)
        {
            var outcome = _component.Analyze(run);
            var meta = new RunMeta<Outcomes>(
                run.Worldname,
                run.World.CreatedOn,
                MetaFeatures.Outcome,
                outcome);

            _writer.Upsert(meta);

            return outcome;
        }
    }
}
