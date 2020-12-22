using LiteDB;
using System.IO;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class LiteAnalyticsDatabaseHandler : IAnalyticsDatabaseHandler
    {
        private const string DatabaseFolder = "database";
        private const string DatabaseFilename = "analytics.litedb";

        private LiteDatabase database { get; }

        public LiteAnalyticsDatabaseHandler(IConfigService config)
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
