using LiteDB;
using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class TimingLiteStore : ITimingStore
    {
        private ILiteCollection<TimingAnalytics> _collection;

        public TimingLiteStore(IDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<TimingAnalytics>();

        public void Add(TimingAnalytics timings)
            => _collection.Insert(timings);

        public void Update(TimingAnalytics timings)
            => _collection.Update(timings);

        public bool Exists(string worldname)
            => _collection.Exists(x => x.Worldname == worldname);

        public IEnumerable<TimingAnalytics> Get()
            => _collection.FindAll();

        public IEnumerable<TimingAnalytics> Get(DateTime from, DateTime to)
            => _collection.Find(x => x.StartedOn >= from && x.StartedOn < to);

        public TimingAnalytics Get(string worldname)
            => _collection.FindOne(x => x.Worldname == worldname);

        public IEnumerable<TimingAnalytics> Get(int lastN)
            => _collection.Query()
                .OrderByDescending(x => x.StartedOn)
                .Limit(lastN)
                .ToEnumerable();

        public int Count()
            => _collection.Count();
    }
}
