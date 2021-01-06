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

        private IEnumerable<TimingStats> groupSections(Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> timings)
        {
            var sections = new List<TimingStats>();

            foreach (var feature in timings)
            {
                var stats = new TimingStats(feature.Key);

                if (feature.Value.Count() == 0)
                {
                    sections.Add(stats);
                    continue;
                }

                var min = feature.Value.Min(x => x.Value);
                var max = feature.Value.Max(x => x.Value);
                var avg = feature.Value.Average(x => x.Value);
                var sd = calculateStandardDeviation(feature.Value);

                stats.Minimum = TimeSpan.FromMilliseconds(min);
                stats.Maximum = TimeSpan.FromMilliseconds(max);
                stats.Average = TimeSpan.FromMilliseconds(avg);
                stats.StandardDeviation = TimeSpan.FromMilliseconds(sd);

                sections.Add(stats);
            }

            return sections;
        }

        private double calculateStandardDeviation(IEnumerable<RunMeta<int>> feature)
        {
            var average = (int)feature.Average(x => x.Value);
            var sumOfSquaresOfDifferences = feature.Select(x => ((long)x.Value - average) * ((long)x.Value - average)).Sum();
            var standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / feature.Count());
            return standardDeviation;
        }
    }
}
