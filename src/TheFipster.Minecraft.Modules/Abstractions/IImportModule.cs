using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public interface IImportModule
    {
        RunImport Import(WorldInfo world);
    }
}
