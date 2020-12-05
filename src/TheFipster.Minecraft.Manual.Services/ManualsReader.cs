using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Manual.Services
{
    public class ManualsReader : IManualsReader
    {
        private readonly ILiteCollection<RunManuals> _collection;

        public ManualsReader(IManualDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunManuals>();

        public int Count()
            => _collection.Count();

        public IEnumerable<RunManuals> Get()
            => _collection.FindAll();

        public RunManuals Get(string name)
            => _collection.FindOne(x => x.Worldname == name);
    }
}
