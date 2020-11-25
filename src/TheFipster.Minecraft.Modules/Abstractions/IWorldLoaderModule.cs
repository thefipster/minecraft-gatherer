using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public interface IWorldLoaderModule
    {
        IEnumerable<WorldInfo> Load(bool overwrite);

    }
}
