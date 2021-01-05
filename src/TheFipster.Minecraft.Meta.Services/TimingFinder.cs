using LiteDB;
using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingFinder : ITimingFinder
    {
        private readonly ILiteCollection<RunMeta<int>> _collection;

        public TimingFinder(IMetaDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunMeta<int>>("MetaTiming");

        public IEnumerable<RunMeta<int>> Get()
            => _collection.FindAll();

        public IEnumerable<RunMeta<int>> Get(DateTime inclusiveStart, DateTime exclusiveEnd)
            => _collection.Find(x =>
                x.Timestamp >= inclusiveStart
                && x.Timestamp < exclusiveEnd);
    }
}
