using LiteDB;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
    }
}
