using LiteDB;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IManualDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
