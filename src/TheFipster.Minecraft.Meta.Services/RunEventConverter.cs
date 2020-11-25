using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class RunEventConverter : IRunEventConverter

    {
        public IList<RunEvent> Convert(RunImport run) => new List<RunEvent>();
    }
}
