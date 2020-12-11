using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class NbtLoader : INbtLoader
    {
        public NbtData Load(WorldInfo world)
            => new NbtData();
    }
}
