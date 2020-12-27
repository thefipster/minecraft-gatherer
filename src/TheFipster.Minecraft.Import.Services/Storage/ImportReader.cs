using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class ImportReader : IImportReader
    {
        private readonly ILiteCollection<RunImport> _collection;

        public ImportReader(IImportDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunImport>();

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
