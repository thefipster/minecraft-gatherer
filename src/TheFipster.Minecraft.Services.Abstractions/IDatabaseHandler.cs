using LiteDB;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
    }
}
