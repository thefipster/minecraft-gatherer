using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public interface IImportRunModule
    {
        RunImport Import(WorldInfo world);
    }
}
