using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IPeriodSlicer
    {
        IEnumerable<TimePeriod> Slice(Periods period);
    }
}
