using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules.Abstractions
{
    public interface IEnhanceModule
    {
        RunImport Enhance(RunImport run);
    }
}
