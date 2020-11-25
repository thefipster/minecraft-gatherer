using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface IRunEventConverter
    {
        IList<RunEvent> Convert(RunImport run);
    }
}
