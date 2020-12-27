using LiteDB;
using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class OutcomeFinder : IOutcomeFinder
    {
        private readonly ILiteCollection<RunMeta<Outcomes>> _collection;

        public OutcomeFinder(IMetaDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunMeta<Outcomes>>("MetaOutcomes");

        public IEnumerable<RunMeta<Outcomes>> Get()
            => _collection.FindAll();

        public IEnumerable<RunMeta<Outcomes>> Get(DateTime inclusiveStart, DateTime exclusiveEnd)
            => _collection.Find(x =>
                x.Timestamp >= inclusiveStart
                && x.Timestamp < exclusiveEnd);
    }
}
