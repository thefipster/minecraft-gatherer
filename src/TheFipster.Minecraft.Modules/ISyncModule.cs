using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public interface ISyncModule
    {
        IEnumerable<RunImport> Synchronize(bool withForce = false);
    }
}
