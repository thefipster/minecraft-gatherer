using System;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class ManualEndTimeAdjuster : IManualEndTimeAdjuster
    {
        private readonly IManualsReader _manualsReader;
        private readonly IAnalyticsReader _analyticsReader;
        private readonly IAnalyticsWriter _analyticsWriter;

        public ManualEndTimeAdjuster(IManualsReader manualsReader, IAnalyticsReader analyticsReader, IAnalyticsWriter analyticsWriter)
        {
            _manualsReader = manualsReader;
            _analyticsReader = analyticsReader;
            _analyticsWriter = analyticsWriter;
        }

        public void Adjust(RunManuals manuals)
        {
            var analytics = _analyticsReader.Get(manuals.Worldname);
            analytics.Timings = applyManualInput(analytics.Timings, manuals);
            _analyticsWriter.Upsert(analytics);
        }

        public TimingAnalytics Adjust(TimingAnalytics timings)
        {
            var manuals = _manualsReader.Get(timings.Worldname);

            if (manuals == null)
                return timings;

            timings = applyManualInput(timings, manuals);
            return timings;
        }

        private TimingAnalytics applyManualInput(TimingAnalytics analytics, RunManuals manuals)
        {
            if (!manuals.RuntimeInMs.HasValue)
                return analytics;

            if (!analytics.FinishedOn.HasValue)
                return analytics;

            if (!analytics.Events.Any(x => x.Section == Sections.TheEnd))
                return analytics;

            var newTimeInEnd = generateNewEndTime(analytics, manuals);

            if (newTimeInEnd.TotalSeconds <= 30)
                return analytics;

            analytics.RunTime = TimeSpan.FromMilliseconds(manuals.RuntimeInMs.Value);
            analytics.Events.First(x => x.Section == Sections.TheEnd).Time = newTimeInEnd;
            analytics.Events.First(x => x.Section == Sections.TheEnd).End = analytics.Events.First(x => x.Section == Sections.TheEnd).Start + newTimeInEnd;
            analytics.ManualOverride = true;

            return analytics;
        }

        private TimeSpan generateNewEndTime(TimingAnalytics analytics, RunManuals manuals)
        {
            var timeWithoutEnd = analytics.Events.Where(x => x.IsSubSplit == false).Where(x => x.Section != Sections.TheEnd).Sum(x => x.Time.TotalMilliseconds);
            var timeInEnd = manuals.RuntimeInMs.Value - timeWithoutEnd;
            return TimeSpan.FromMilliseconds(timeInEnd);
        }
    }
}
