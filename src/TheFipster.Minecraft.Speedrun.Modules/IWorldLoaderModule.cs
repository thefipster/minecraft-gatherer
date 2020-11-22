using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public interface IWorldLoaderModule
    {
        IEnumerable<WorldInfo> Load(bool overwrite);

    }
}
