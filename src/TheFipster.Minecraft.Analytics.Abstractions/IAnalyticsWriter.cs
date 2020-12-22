using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IAnalyticsWriter
    {
        void Upsert(RunAnalytics analytics);

        bool Exists(string name);

        int Count();
    }
}
