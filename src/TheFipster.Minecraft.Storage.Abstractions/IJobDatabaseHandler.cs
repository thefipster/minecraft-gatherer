using LiteDB;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IJobDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
