using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class TimingStatsExtender : ITimingStatsExtender
    {
        private readonly ITimingFinder _finder;
        private readonly IMapper<Sections, MetaFeatures> _mapper;
        private readonly IPeriodSlicer _slicer;

        public TimingStatsExtender(ITimingFinder finder, IMapper<Sections, MetaFeatures> mapper, IPeriodSlicer slicer)
        {
            _finder = finder;
            _mapper = mapper;
            _slicer = slicer;
        }

        public IEnumerable<TimingStats> Extend()
        {
            var timings = _finder.Get();
            var result = groupSections(timings);
            return result;
        }

        public IEnumerable<RunMeta<int>> Extend(Sections section)
        {
            var feature = _mapper.Map(section);
            var timings = _finder.Get(feature);
            return timings;
        }

        public Dictionary<TimePeriod, IEnumerable<RunMeta<int>>> Extend(Sections section, Periods period)
        {
            var result = new Dictionary<TimePeriod, IEnumerable<RunMeta<int>>>();

            var feature = _mapper.Map(section);
            var periods = _slicer.Slice(period);

            foreach (var slice in periods)
            {
                var timings = _finder.Get(feature, slice.Start, slice.End);
                result.Add(slice, timings);

            }

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
