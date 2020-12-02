using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class AnalyticsLiteStore : IAnalyticsStore
    {
        private ILiteCollection<RunAnalytics> _collection;
        private readonly int _initialIndex;

        public AnalyticsLiteStore(IDatabaseHandler databaseHandler, IConfigService configService)
        {
            _collection = databaseHandler.GetCollection<RunAnalytics>();
            _initialIndex = configService.InitialRunIndex;
        }

        public void Insert(RunAnalytics analytics)
            => _collection.Insert(analytics);

        public void Update(RunAnalytics analytics)
            => _collection.Update(analytics);

        public void Upsert(RunAnalytics analytics)
        {
            if (Exists(analytics.Worldname))
                Update(analytics);
            else
                Insert(analytics);
        }

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

        public void Index()
        {
            var analytics = Get();

            var index = _initialIndex + 1;
            foreach (var analytic in analytics.OrderBy(x => x.Timings.StartedOn))
            {
                if (analytic.Outcome == Outcomes.Unknown || analytic.Outcome == Outcomes.Untouched)
                    continue;

                analytic.Index = index;
                Update(analytic);
                index++;
            }
        }
    }
}
