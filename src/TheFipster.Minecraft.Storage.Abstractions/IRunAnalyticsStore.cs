using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IAnalyticsStore
    {
        void Insert(RunAnalytics analytics);
        void Update(RunAnalytics analytics);
        void Upsert(RunAnalytics analytics);
        bool Exists(string name);
        int Count();
        IEnumerable<RunAnalytics> Get();
        RunAnalytics Get(string name);
        void Index();
        RunAnalytics Get(int index);
    }
}
