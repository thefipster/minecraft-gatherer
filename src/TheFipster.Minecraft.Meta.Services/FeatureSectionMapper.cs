using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class FeatureSectionMapper : IMapper<Sections, MetaFeatures>
    {
        private readonly Dictionary<Sections, MetaFeatures> map = new Dictionary<Sections, MetaFeatures>
        {
            { Sections.BlazeRod, MetaFeatures.BlazeRod },
            { Sections.Fortress, MetaFeatures.Fortress },
            { Sections.Nether, MetaFeatures.Nether },
            { Sections.Search, MetaFeatures.Search },
            { Sections.Spawn, MetaFeatures.Spawn },
            { Sections.Stronghold, MetaFeatures.Stronghold },
            { Sections.TheEnd, MetaFeatures.TheEnd }
        };

        public Sections Map(MetaFeatures feature)
        {
            if (!map.Any(x => x.Value == feature))
                throw new Exception("Feature can not be mapped to section.");

            var entry = map.First(x => x.Value == feature);
            return entry.Key;
        }

        public MetaFeatures Map(Sections section)
            => map[section];
    }
}
