using LiteDB;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
    }
}
