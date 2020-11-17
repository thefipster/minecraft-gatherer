using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public interface IImportModule
    {
        IEnumerable<RunInfo> Import(bool overwrite = false);
    }
}
