using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Manual.Abstractions;

namespace TheFipster.Minecraft.Manual.Services
{
    public class LiteManualDatabaseHandler : IManualDatabaseHandler
    {
        private const string DatabaseFolder = "database";
        private const string DatabaseFilename = "manual.litedb";

        private LiteDatabase database { get; }

        public LiteManualDatabaseHandler(IConfigService config)
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
