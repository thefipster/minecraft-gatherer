using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface IOutomeFinder
    {
        IEnumerable<RunMeta<Outcomes>> Get();
        IEnumerable<RunMeta<Outcomes>> Get(DateTime inclusiveStart, DateTime exclusiveEnd);
    }
}
