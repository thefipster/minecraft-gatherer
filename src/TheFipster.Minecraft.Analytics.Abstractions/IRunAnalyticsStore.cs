using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IRunAnalyticsStore
    {
        void Add(RunAnalytics run);
        void Update(RunAnalytics run);
        bool Exists(string name);
        int Count();
        IEnumerable<RunAnalytics> Get();
        RunAnalytics Get(string name);
    }
}
