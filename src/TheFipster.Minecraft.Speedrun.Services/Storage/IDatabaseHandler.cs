using LiteDB;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
    }
}
