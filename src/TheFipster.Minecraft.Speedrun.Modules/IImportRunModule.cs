using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public interface IImportRunModule
    {
        RunInfo Import(WorldInfo world);
    }
}
