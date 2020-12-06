using LiteDB;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Manual.Services
{
    public class ManualsWriter : IManualsWriter
    {
        private readonly ILiteCollection<RunManuals> _collection;

        public ManualsWriter(IManualDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunManuals>();

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public void Upsert(RunManuals manuals)
        {
            if (Exists(manuals.Worldname))
                update(manuals);
            else
                insert(manuals);
        }

        private void insert(RunManuals manuals)
            => _collection.Insert(manuals);

        private void update(RunManuals manuals)
            => _collection.Update(manuals);
    }
}
