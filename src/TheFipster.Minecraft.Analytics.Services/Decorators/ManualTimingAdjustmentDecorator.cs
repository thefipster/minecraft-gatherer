using System;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class ManualTimingAdjustmentDecorator : IManualsWriter
    {
        private readonly IManualsWriter _component;
        private readonly IAnalyticsReader _reader;
        private readonly IAnalyticsWriter _writer;

        public ManualTimingAdjustmentDecorator(IManualsWriter component, IAnalyticsReader reader, IAnalyticsWriter writer)
        {
            _component = component;
            _reader = reader;
            _writer = writer;
        }

        public bool Exists(string name)
            => _component.Exists(name);

        public void Upsert(RunManuals manuals)
        {
            _component.Upsert(manuals);

            if (!manuals.RuntimeInMs.HasValue)
                return;

            var analytics = _reader.Get(manuals.Worldname);

            if (!analytics.Timings.FinishedOn.HasValue)
                return;

            if (!analytics.Timings.Events.Any(x => x.Section == Sections.TheEnd))
                return;

            analytics.Timings.RunTime = TimeSpan.FromMilliseconds(manuals.RuntimeInMs.Value);

            var timeWithoutEnd = analytics.Timings.Events.Where(x => x.IsSubSplit == false).Where(x => x.Section != Sections.TheEnd).Sum(x => x.Time.TotalMilliseconds);
            var timeInEnd = manuals.RuntimeInMs.Value - timeWithoutEnd;

            if (timeInEnd <= 0)
                return;

            var newTimeInEnd = TimeSpan.FromMilliseconds(timeInEnd);

            analytics.Timings.Events.First(x => x.Section == Sections.TheEnd).Time = newTimeInEnd;
            analytics.Timings.Events.First(x => x.Section == Sections.TheEnd).End = analytics.Timings.Events.First(x => x.Section == Sections.TheEnd).Start + newTimeInEnd;
            analytics.Timings.ManualOverride = true;

            _writer.Upsert(analytics);
        }
    }
}
