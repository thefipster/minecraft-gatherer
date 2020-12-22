using LiteDB;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IAnalyticsDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
