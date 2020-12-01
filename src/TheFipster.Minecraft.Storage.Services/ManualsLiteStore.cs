using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Manual.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class ManualsLiteStore : IManualsStore
    {
        private ILiteCollection<RunManuals> _collection;

        public ManualsLiteStore(IDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunManuals>();

        public int Count()
            => _collection.Count();

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public IEnumerable<RunManuals> Get()
            => _collection.FindAll();

        public RunManuals Get(string name)
            => _collection.FindOne(x => x.Worldname == name);

        public void Insert(RunManuals manuals)
            => _collection.Insert(manuals);

        public void Update(RunManuals manuals)
            => _collection.Update(manuals);

        public void Upsert(RunManuals manuals)
        {
            if (Exists(manuals.Worldname))
                Update(manuals);
            else
                Insert(manuals);
        }
    }
}
