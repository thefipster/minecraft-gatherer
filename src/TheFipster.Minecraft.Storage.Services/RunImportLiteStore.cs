using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class ImportLiteStore : IImportStore
    {
        private ILiteCollection<RunImport> _collection;

        public ImportLiteStore(IDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunImport>();

        public void Insert(RunImport import)
            => _collection.Insert(import);

        public void Update(RunImport import)
            => _collection.Update(import);

        public void Upsert(RunImport import)
        {
            if (Exists(import.Worldname))
                Update(import);
            else
                Insert(import);
        }

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public int Count()
            => _collection.Count();

        public IEnumerable<RunImport> Get()
            => _collection.FindAll();

        public RunImport Get(string name)
            => _collection.FindOne(x => x.Worldname == name);
    }
}
