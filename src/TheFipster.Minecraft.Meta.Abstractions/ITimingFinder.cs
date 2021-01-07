using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface ITimingFinder
    {
        Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get();
        Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get(DateTime inclusiveStart, DateTime exclusiveEnd);

        IEnumerable<RunMeta<int>> Get(MetaFeatures feature);
        IEnumerable<RunMeta<int>> Get(MetaFeatures feature, DateTime inclusiveStart, DateTime exclusiveEnd);
    }
}
