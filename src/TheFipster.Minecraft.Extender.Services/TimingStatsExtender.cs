using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class TimingStatsExtender : ITimingStatsExtender
    {
        private readonly ITimingFinder _finder;

        public TimingStatsExtender(ITimingFinder finder)
        {
            _finder = finder;
        }

        public IEnumerable<TimingStats> Extend()
        {
            var timings = _finder.Get();
            var result = groupSections(timings);
            return result;
        }

        public IEnumerable<TimingStats> Extend(DateTime inclusiveStart, DateTime exclusiveEnd)
        {
            var timings = _finder.Get(inclusiveStart, exclusiveEnd);
            var result = groupSections(timings);
            return result;
        }

        private IEnumerable<TimingStats> groupSections(IEnumerable<RunMeta<int>> timings)
        {
            var sections = new List<TimingStats>();
            var grouped = timings.GroupBy(x => x.Feature);

            foreach (var feature in grouped)
            {
                var min = feature.Min(x => x.Value);
                var max = feature.Max(x => x.Value);
                var avg = feature.Average(x => x.Value);
                var sd = calculateStandardDeviation(feature.AsEnumerable());


                var stats = new TimingStats(feature.Key)
                {
                    Minimum = TimeSpan.FromMilliseconds(min),
                    Maximum = TimeSpan.FromMilliseconds(max),
                    Average = TimeSpan.FromMilliseconds(avg),
                    StandardDeviation = TimeSpan.FromMilliseconds(sd)
                };

                sections.Add(stats);
            }

            return sections;
        }


        private double calculateStandardDeviation(IEnumerable<RunMeta<int>> feature)
        {
            var average = (int)feature.Average(x => x.Value);
            var sumOfSquaresOfDifferences = feature.Select(x => ((long)x.Value - (long)average) * ((long)x.Value - (long)average)).Sum();
            var standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / feature.Count());
            return standardDeviation;
        }
    }
}
