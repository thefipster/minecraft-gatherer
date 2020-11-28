using System.Collections.Generic;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IQuickestEventEnhancer
    {
        IEnumerable<FirstEvent> Enhance(RunImport run);

    }
}
