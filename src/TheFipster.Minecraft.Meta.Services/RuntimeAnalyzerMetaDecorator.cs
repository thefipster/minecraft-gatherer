using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class RuntimeAnalyzerMetaDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;
        private readonly IRuntimeWriter _runtimeWriter;

        public RuntimeAnalyzerMetaDecorator(
            ITimingAnalyzer timingAnalyzer,
            IRuntimeWriter runtimeWriter)
        {
            _component = timingAnalyzer;
            _runtimeWriter = runtimeWriter;
        }

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var analytics = _component.Analyze(run);

            if (analytics.FinishedOn.HasValue)
                extractRuntime(analytics);

            return analytics;
        }

        private void extractRuntime(TimingAnalytics analytics)
        {
            var runtime = analytics.RunTime;
            var meta = new RunMeta<int>(
                analytics.Worldname,
                analytics.StartedOn,
                MetaFeatures.Runtime,
                (int)runtime.TotalMilliseconds);

            _runtimeWriter.Upsert(meta);
        }
    }
}
