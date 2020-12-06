using System.Collections.Generic;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IQuickestEventExtender
    {
        IEnumerable<FirstEvent> Extend(RunImport run);

    }
}
