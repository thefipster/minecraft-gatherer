using LiteDB;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface IMetaDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
