using LiteDB;
using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Meta.Services
{
    public class OutcomeFinder : IOutcomeFinder
    {
        private ILiteCollection<RunMeta<Outcomes>> _collection;

        public OutcomeFinder(ISyncDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunMeta<Outcomes>>("MetaOutcomes");

        public IEnumerable<RunMeta<Outcomes>> Get()
            => _collection.FindAll();

        public IEnumerable<RunMeta<Outcomes>> Get(DateTime inclusiveStart, DateTime exclusiveEnd)
            => _collection.Find(x =>
                x.Timestamp >= inclusiveStart
                && x.Timestamp < exclusiveEnd);
    }
}
