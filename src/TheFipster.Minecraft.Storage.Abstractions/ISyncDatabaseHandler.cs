using LiteDB;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface ISyncDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
