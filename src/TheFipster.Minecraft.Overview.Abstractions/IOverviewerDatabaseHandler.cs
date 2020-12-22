using LiteDB;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IOverviewerDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
