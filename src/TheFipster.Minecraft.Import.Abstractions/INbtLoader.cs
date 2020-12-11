using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface INbtLoader
    {
        NbtData Load(WorldInfo world);
    }
}
