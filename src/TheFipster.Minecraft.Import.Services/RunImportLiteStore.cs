using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Services.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class RunImportLiteStore : IRunImportStore
    {
        private ILiteCollection<RunImport> _collection;

        public RunImportLiteStore(IDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunImport>();

        public void Add(RunImport run)
            => _collection.Insert(run);

        public void Update(RunImport run)
            => _collection.Update(run);

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
