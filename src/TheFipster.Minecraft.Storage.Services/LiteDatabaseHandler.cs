using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class LiteDatabaseHandler : IDatabaseHandler
    {
        private const string DatabaseFilename = "runs.litedb";
        private const string DatabaseFolder = "database";

        private LiteDatabase _database;

        public LiteDatabaseHandler(IConfigService config)
        {
            var databaseFilepath = Path.Combine(
                config.DataLocation.FullName,
                DatabaseFolder,
                DatabaseFilename);

            _database = new LiteDatabase(databaseFilepath);
        }

        public ILiteCollection<T> GetCollection<T>()
            => _database.GetCollection<T>();
    }
}
