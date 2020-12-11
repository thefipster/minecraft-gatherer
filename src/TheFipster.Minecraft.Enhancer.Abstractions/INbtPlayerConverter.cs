using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Abstractions
{
    public interface INbtPlayerConverter
    {
        ICollection<NbtPlayer> Convert(RunImport run);
    }
}
