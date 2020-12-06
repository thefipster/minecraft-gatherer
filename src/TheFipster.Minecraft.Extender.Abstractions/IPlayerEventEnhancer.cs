using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IPlayerEventExtender
    {
        Dictionary<string, IEnumerable<RunEvent>> Extend(RunImport run);
    }
}
