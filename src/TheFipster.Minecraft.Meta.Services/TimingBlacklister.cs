using LiteDB;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingBlacklister : ITimingBlacklister
    {
        private readonly ILiteCollection<BlacklistEntry> _collection;

        public TimingBlacklister(IMetaDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<BlacklistEntry>("MetaTimingBlacklist");

        public void Add(string worldname)
            => _collection.Upsert(new BlacklistEntry(worldname));

        public void Remove(string worldname)
            => _collection.Delete(new BsonValue(worldname));

        public bool IsBlacklisted(string worldname)
        {
            if (_collection.FindById(new BsonValue(worldname)) != null)
                return true;

            return false;
        }
    }
}
