using LiteDB;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LiteDatabaseHandler : IDatabaseHandler
    {
        private const string DatabaseFilename = "run.litedb";
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
