using LiteDB;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Meta.Abstractions;

namespace TheFipster.Minecraft.Meta.Services
{
    public class LiteMetaDatabaseHandler : IMetaDatabaseHandler
    {
        private const string DatabaseFolder = "database";
        private const string DatabaseFilename = "meta.litedb";

        private LiteDatabase database { get; }

        public LiteMetaDatabaseHandler(IConfigService config)
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
