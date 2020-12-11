using LiteDB;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class LiteJobDatabaseHandler : ISyncDatabaseHandler
    {
        private const string DatabaseFilename = "jobs.litedb";

        private readonly LiteDatabaseHandler _handler;

        public LiteJobDatabaseHandler(IConfigService config)
            => _handler = new LiteDatabaseHandler(config, DatabaseFilename);

        public ILiteCollection<T> GetCollection<T>()
            => _handler.Database.GetCollection<T>();

        public ILiteCollection<T> GetCollection<T>(string name)
            => _handler.Database.GetCollection<T>(name);
    }
}
