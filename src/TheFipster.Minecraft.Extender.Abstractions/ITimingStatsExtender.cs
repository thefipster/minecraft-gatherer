using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface ITimingStatsExtender
    {
        IEnumerable<TimingStats> Extend();
        IEnumerable<TimingStats> Extend(DateTime inclusiveStart, DateTime exclusiveEnd);
        Dictionary<TimePeriod, IEnumerable<RunMeta<int>>> Extend(Sections section, Periods period);
        IEnumerable<RunMeta<int>> Extend(Sections section);

    }
}
