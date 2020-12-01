using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Enhancer.Domain;

namespace TheFipster.Minecraft.Enhancer.Abstractions
{
    public interface IRunEventConverter
    {
        IList<RunEvent> Convert(RunImport run);
    }
}
