using LiteDB;

namespace TheFipster.Minecraft.Manual.Abstractions
{
    public interface IManualDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
