using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface ITimingStatsExtender
    {
        IEnumerable<TimingStats> Extend();
        IEnumerable<TimingStats> Extend(DateTime inclusiveStart, DateTime exclusiveEnd);
    }
}
