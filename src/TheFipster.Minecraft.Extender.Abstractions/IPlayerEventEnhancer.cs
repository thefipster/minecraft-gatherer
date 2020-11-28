using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IPlayerEventEnhancer
    {
        Dictionary<string, IEnumerable<RunEvent>> Enhance(RunImport run);
    }
}
