using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Abstractions
{
    public interface INbtLevelConverter
    {
        NbtLevel Convert(RunImport run);
    }
}
