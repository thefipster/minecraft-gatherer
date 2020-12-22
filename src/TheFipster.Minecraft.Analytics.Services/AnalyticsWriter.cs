using LiteDB;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class AnalyticsWriter : IAnalyticsWriter
    {
        private readonly ILiteCollection<RunAnalytics> _collection;

        public AnalyticsWriter(IAnalyticsDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunAnalytics>();

        public void Upsert(RunAnalytics analytics)
        {
            if (Exists(analytics.Worldname))
                _collection.Update(analytics);
            else
                _collection.Insert(analytics);
        }

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public int Count()
            => _collection.Count();
    }
}
