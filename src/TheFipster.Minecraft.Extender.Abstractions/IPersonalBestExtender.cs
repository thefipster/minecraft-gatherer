using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IPersonalBestExtender
    {
        IEnumerable<RunAnalytics> Extend(int n);
    }
}
