using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IImportWriter
    {
        void Upsert(RunImport import);

        bool Exists(string name);

        int Count();
    }
}
