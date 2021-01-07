using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class BestTimingsExtender : IBestTimingsExtender
    {
        private readonly ITimingFinder _timingsFinder;
        private readonly IAnalyticsReader _analyticsReader;
        private readonly IMapper<Sections, MetaFeatures> _mapper;

        public BestTimingsExtender(ITimingFinder timingsFinder, IAnalyticsReader analyticsReader, IMapper<Sections, MetaFeatures> mapper)
        {
            _timingsFinder = timingsFinder;
            _analyticsReader = analyticsReader;
            _mapper = mapper;
        }

        public Dictionary<Sections, RunTiming> Extend()
        {
            var fastestTimes = new Dictionary<Sections, RunTiming>();

            fastestTimes = addSectionToDictionary(fastestTimes, Sections.Spawn);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.Nether);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.Fortress);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.BlazeRod);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.Search);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.Stronghold);
            fastestTimes = addSectionToDictionary(fastestTimes, Sections.TheEnd);

            return fastestTimes;
        }

        private Dictionary<Sections, RunTiming> addSectionToDictionary(Dictionary<Sections, RunTiming> dict, Sections section)
        {
            var timing = getTimingForSection(section);
            dict.Add(section, timing);
            return dict;
        }

        private RunTiming getTimingForSection(Sections section)
        {
            var feature = _mapper.Map(section);
            var times = _timingsFinder.Get(feature);
            var fastest = times.OrderBy(x => x.Value).FirstOrDefault();
            var run = fastest != null
                ? _analyticsReader.Get(fastest.Worldname)
                : null;

            return new RunTiming(section, fastest, run);
        }
    }
}
