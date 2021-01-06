using LiteDB;
using System;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingWriter : ITimingWriter
    {
        private readonly ILiteCollection<RunMeta<int>> _spawnCollection;
        private readonly ILiteCollection<RunMeta<int>> _netherCollection;
        private readonly ILiteCollection<RunMeta<int>> _fortressCollection;
        private readonly ILiteCollection<RunMeta<int>> _blazeCollection;
        private readonly ILiteCollection<RunMeta<int>> _searchCollection;
        private readonly ILiteCollection<RunMeta<int>> _strongholdCollection;
        private readonly ILiteCollection<RunMeta<int>> _endCollection;

        public TimingWriter(IMetaDatabaseHandler databaseHandler)
        {
            _spawnCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaSpawnTiming");
            _netherCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaNetherTiming");
            _fortressCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaFortressTiming");
            _blazeCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaBlazeTiming");
            _searchCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaSearchTiming");
            _strongholdCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaStrongholdTiming");
            _endCollection = databaseHandler.GetCollection<RunMeta<int>>("MetaEndTiming");
        }

        public bool Exists(RunMeta<int> meta)
            => Exists(meta.Worldname, meta.Feature);

        public bool Exists(string worldname, MetaFeatures feature)
        {
            switch (feature)
            {
                case MetaFeatures.BlazeRod:
                    {
                        return _blazeCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.Fortress:
                    {
                        return _fortressCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.Nether:
                    {
                        return _netherCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.Search:
                    {
                        return _searchCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.Spawn:
                    {
                        return _spawnCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.Stronghold:
                    {
                        return _strongholdCollection.Exists(x => x.Worldname == worldname);
                    }
                case MetaFeatures.TheEnd:
                    {
                        return _endCollection.Exists(x => x.Worldname == worldname);
                    }
                default:
                    {
                        throw new Exception($"Feature {feature} now allowed.");
                    }
            }
        }

        public void Upsert(RunMeta<int> meta)
        {
            if (Exists(meta))
                update(meta);
            else
                insert(meta);
        }

        private void insert(RunMeta<int> meta)
        {
            switch (meta.Feature)
            {
                case MetaFeatures.BlazeRod:
                    {
                        _blazeCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.Fortress:
                    {
                        _fortressCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.Nether:
                    {
                        _netherCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.Search:
                    {
                        _searchCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.Spawn:
                    {
                        _spawnCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.Stronghold:
                    {
                        _strongholdCollection.Insert(meta);
                        break;
                    }
                case MetaFeatures.TheEnd:
                    {
                        _endCollection.Insert(meta);
                        break;
                    }
                default:
                    {
                        throw new Exception($"Feature {meta.Feature} now allowed.");
                    }
            }
        }

        private void update(RunMeta<int> meta)
        {
            switch (meta.Feature)
            {
                case MetaFeatures.BlazeRod:
                    {
                        _blazeCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.Fortress:
                    {
                        _fortressCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.Nether:
                    {
                        _netherCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.Search:
                    {
                        _searchCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.Spawn:
                    {
                        _spawnCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.Stronghold:
                    {
                        _strongholdCollection.Update(meta);
                        break;
                    }
                case MetaFeatures.TheEnd:
                    {
                        _endCollection.Update(meta);
                        break;
                    }
                default:
                    {
                        throw new Exception($"Feature {meta.Feature} now allowed.");
                    }
            }
        }
    }
}
