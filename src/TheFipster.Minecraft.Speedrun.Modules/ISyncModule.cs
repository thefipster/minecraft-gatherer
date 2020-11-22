using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public interface ISyncModule
    {
        IEnumerable<RunInfo> Synchronize(bool withForce);
    }
}
