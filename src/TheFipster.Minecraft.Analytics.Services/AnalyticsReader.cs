using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class AnalyticsReader : IAnalyticsReader
    {
        private readonly ILiteCollection<RunAnalytics> _collection;

        public AnalyticsReader(IAnalyticsDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunAnalytics>();

        public IEnumerable<RunAnalytics> Get()
            => _collection.FindAll();

        public RunAnalytics Get(string name)
            => _collection.FindOne(x => x.Worldname == name);

        public RunAnalytics Get(int index)
            => _collection.FindOne(x => x.Index == index);

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public int Count()
            => _collection.Count();
    }
}
