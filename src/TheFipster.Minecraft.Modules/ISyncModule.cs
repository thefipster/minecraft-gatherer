using System.Collections.Generic;
using TheFipster.Minecraft.Modules.Models;

namespace TheFipster.Minecraft.Modules
{
    public interface ISyncModule
    {
        IEnumerable<WorldSync> Synchronize(bool withForce = false);
    }
}
