using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface ITimingFinder
    {
        IEnumerable<RunMeta<int>> Get();
        IEnumerable<RunMeta<int>> Get(DateTime inclusiveStart, DateTime exclusiveEnd);
    }
}
