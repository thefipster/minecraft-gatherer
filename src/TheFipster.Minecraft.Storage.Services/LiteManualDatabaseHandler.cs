using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class LiteManualDatabaseHandler : IManualDatabaseHandler
    {
        private const string DatabaseFilename = "manual.litedb";
        private const string DatabaseFolder = "database";

        private LiteDatabase _database;

        public LiteManualDatabaseHandler(IConfigService config)
        {
            var databaseFilepath = Path.Combine(
                config.DataLocation.FullName,
                DatabaseFolder,
                DatabaseFilename);

            _database = new LiteDatabase(databaseFilepath);
        }

        public ILiteCollection<T> GetCollection<T>()
            => _database.GetCollection<T>();

        public ILiteCollection<T> GetCollection<T>(string name)
            => _database.GetCollection<T>(name);
    }
}
