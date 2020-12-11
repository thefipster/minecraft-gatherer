using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    internal class LiteDatabaseHandler
    {
        private const string DatabaseFolder = "database";

        public LiteDatabaseHandler(IConfigService config, string filename)
        {
            var databaseFilepath = Path.Combine(
                config.DataLocation.FullName,
                DatabaseFolder,
                filename);

            Database = new LiteDatabase(databaseFilepath);
        }

        public LiteDatabase Database { get; }
    }
}
