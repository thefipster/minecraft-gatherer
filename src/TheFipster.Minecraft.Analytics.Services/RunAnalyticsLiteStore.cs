using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class RunAnalyticsLiteStore : IRunAnalyticsStore
    {
        private ILiteCollection<RunAnalytics> _collection;

        public RunAnalyticsLiteStore(IDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunAnalytics>();

        public void Add(RunAnalytics analytics)
            => _collection.Insert(analytics);

        public void Update(RunAnalytics analytics)
            => _collection.Update(analytics);

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public int Count()
            => _collection.Count();

        public IEnumerable<RunAnalytics> Get()
            => _collection.FindAll();

        public RunAnalytics Get(string name)
            => _collection.FindOne(x => x.Worldname == name);
    }
}
