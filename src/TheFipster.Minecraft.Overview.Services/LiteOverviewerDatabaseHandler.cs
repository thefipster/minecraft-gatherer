using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;

namespace TheFipster.Minecraft.Overview.Services
{
    public class LiteOverviewerDatabaseHandler : IOverviewerDatabaseHandler
    {
        private const string DatabaseFolder = "database";
        private const string DatabaseFilename = "overviewer.litedb";

        private LiteDatabase database { get; }

        public LiteOverviewerDatabaseHandler(IConfigService config)
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
