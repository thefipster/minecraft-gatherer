using LiteDB;
using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingFinder : ITimingFinder
    {
        private readonly ILiteCollection<RunMeta<int>> _spawnCollection;
        private readonly ILiteCollection<RunMeta<int>> _netherCollection;
        private readonly ILiteCollection<RunMeta<int>> _fortressCollection;
        private readonly ILiteCollection<RunMeta<int>> _blazeCollection;
        private readonly ILiteCollection<RunMeta<int>> _searchCollection;
        private readonly ILiteCollection<RunMeta<int>> _strongholdCollection;
        private readonly ILiteCollection<RunMeta<int>> _endCollection;

        private readonly MetaFeatures[] validFeatures = new[]
        {
            MetaFeatures.BlazeRod,
            MetaFeatures.Fortress,
            MetaFeatures.Nether,
            MetaFeatures.Search,
            MetaFeatures.Spawn,
            MetaFeatures.Stronghold,
            MetaFeatures.TheEnd
        };

        public TimingFinder(IMetaDatabaseHandler databaseHandler)
        {
            _spawnCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaSpawnTiming");
            _netherCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaNetherTiming");
            _fortressCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaFortressTiming");
            _blazeCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaBlazeTiming");
            _searchCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaSearchTiming");
            _strongholdCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaStrongholdTiming");
            _endCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaEndTiming");
        }

        public IEnumerable<RunMeta<int>> Get(MetaFeatures feature)
        {
            switch (feature)
            {
                case MetaFeatures.BlazeRod:
                    {
                        return _blazeCollection.FindAll();
                    }
                case MetaFeatures.Fortress:
                    {
                        return _fortressCollection.FindAll();
                    }
                case MetaFeatures.Nether:
                    {
                        return _netherCollection.FindAll();
                    }
                case MetaFeatures.Search:
                    {
                        return _searchCollection.FindAll();
                    }
                case MetaFeatures.Spawn:
                    {
                        return _spawnCollection.FindAll();
                    }
                case MetaFeatures.Stronghold:
                    {
                        return _strongholdCollection.FindAll();
                    }
                case MetaFeatures.TheEnd:
                    {
                        return _endCollection.FindAll();
                    }
                default:
                    {
                        throw new Exception($"Feature {feature} now allowed.");
                    }
            }
        }

        public Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get()
        {
            var featureTimings = new Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>>();

            foreach (var feature in validFeatures)
            {
                var timings = Get(feature);
                featureTimings.Add(feature, timings);
            }

            return featureTimings;
        }

        public Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>> Get(DateTime inclusiveStart, DateTime exclusiveEnd)
        {
            var featureTimings = new Dictionary<MetaFeatures, IEnumerable<RunMeta<int>>>();

            foreach (var feature in validFeatures)
            {
                var timings = Get(feature, inclusiveStart, exclusiveEnd);
                featureTimings.Add(feature, timings);
            }

            return featureTimings;
        }

        public IEnumerable<RunMeta<int>> Get(MetaFeatures feature, DateTime inclusiveStart, DateTime exclusiveEnd)
        {
            switch (feature)
            {
                case MetaFeatures.BlazeRod:
                    {
                        return _blazeCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.Fortress:
                    {
                        return _fortressCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.Nether:
                    {
                        return _netherCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.Search:
                    {
                        return _searchCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.Spawn:
                    {
                        return _spawnCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.Stronghold:
                    {
                        return _strongholdCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                case MetaFeatures.TheEnd:
                    {
                        return _endCollection.Find(x =>
                            x.Timestamp >= inclusiveStart
                            && x.Timestamp < exclusiveEnd);
                    }
                default:
                    {
                        throw new Exception($"Feature {feature} now allowed.");
                    }
            }
        }
    }
}
