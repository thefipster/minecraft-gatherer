using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IAnalyticsReader
    {
        IEnumerable<RunAnalytics> Get();

        RunAnalytics Get(string name);

        RunAnalytics Get(int index);

        bool Exists(string name);

        int Count();
    }
}
