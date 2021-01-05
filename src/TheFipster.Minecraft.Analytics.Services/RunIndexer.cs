using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class RunIndexer : IRunIndexer
    {
        private readonly IAnalyticsReader _reader;
        private readonly IAnalyticsWriter _writer;

        private readonly int _initialIndex;

        public RunIndexer(IAnalyticsReader reader, IAnalyticsWriter writer, IConfigService configService)
        {
            _reader = reader;
            _writer = writer;

            _initialIndex = configService.InitialRunIndex;
        }

        public void Index()
        {
            var analytics = _reader.Get();

            var index = _initialIndex + 1;
            foreach (var analytic in analytics.OrderBy(x => x.Timings.StartedOn))
            {
                if (analytic.Outcome == Outcomes.Unknown || analytic.Outcome == Outcomes.Untouched)
                    continue;

                analytic.Index = index;
                _writer.Upsert(analytic);
                index++;
            }
        }
    }
}
