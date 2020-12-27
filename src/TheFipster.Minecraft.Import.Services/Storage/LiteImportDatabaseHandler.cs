using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class LiteImportDatabaseHandler : IImportDatabaseHandler
    {
        private const string DatabaseFolder = "database";
        private const string DatabaseFilename = "import.litedb";

        private LiteDatabase database { get; }

        public LiteImportDatabaseHandler(IConfigService config)
        {
            var databaseFilepath = Path.Combine(
                config.DataLocation.FullName,
                DatabaseFolder,
                DatabaseFilename);

            database = new LiteDatabase(databaseFilepath);
        }

        public ILiteCollection<T> GetCollection<T>()
            => database.GetCollection<T>();

        public ILiteCollection<T> GetCollection<T>(string name)
            => database.GetCollection<T>(name);
    }
}
