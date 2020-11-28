using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IRunAnalyticsStore
    {
        void Insert(RunAnalytics analytics);
        void Update(RunAnalytics analytics);
        void Upsert(RunAnalytics analytics);
        bool Exists(string name);
        int Count();
        IEnumerable<RunAnalytics> Get();
        RunAnalytics Get(string name);
    }
}
