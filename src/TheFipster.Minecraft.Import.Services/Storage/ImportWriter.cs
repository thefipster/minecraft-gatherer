using LiteDB;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class ImportWriter : IImportWriter
    {
        private readonly ILiteCollection<RunImport> _collection;

        public ImportWriter(IImportDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunImport>();

        public void Upsert(RunImport import)
        {
            if (Exists(import.Worldname))
                update(import);
            else
                insert(import);
        }

        public bool Exists(string name)
            => _collection.Exists(x => x.Worldname == name);

        public int Count()
            => _collection.Count();

        private void insert(RunImport import)
            => _collection.Insert(import);

        private void update(RunImport import)
            => _collection.Update(import);
    }
}
