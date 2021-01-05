using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingAnalyzerMetaDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;
        private readonly ITimingWriter _writer;

        public TimingAnalyzerMetaDecorator(ITimingAnalyzer component, ITimingWriter writer)
        {
            _component = component;
            _writer = writer;
        }

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);

            if (timings.Events == null)
                return timings;

            writeSection(timings, run, Sections.Spawn, MetaFeatures.Spawn);
            writeSection(timings, run, Sections.Nether, MetaFeatures.Nether);
            writeSection(timings, run, Sections.Fortress, MetaFeatures.Fortress);
            writeSection(timings, run, Sections.BlazeRod, MetaFeatures.BlazeRod);
            writeSection(timings, run, Sections.Search, MetaFeatures.Search);
            writeSection(timings, run, Sections.Stronghold, MetaFeatures.Stronghold);
            writeSection(timings, run, Sections.TheEnd, MetaFeatures.TheEnd);

            return timings;
        }

        private void writeSection(TimingAnalytics timings, RunImport run, Sections section, MetaFeatures feature)
        {
            var timing = timings.Events.FirstOrDefault(x => x.Section == section);
            if (timing != null)
            {
                var meta = new RunMeta<int>(run.Worldname, timing.Start, feature, (int)timing.Time.TotalMilliseconds);
                _writer.Upsert(meta);
            }
        }
    }
}
