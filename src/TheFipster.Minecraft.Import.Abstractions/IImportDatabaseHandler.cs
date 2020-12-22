using LiteDB;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IImportDatabaseHandler
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}
