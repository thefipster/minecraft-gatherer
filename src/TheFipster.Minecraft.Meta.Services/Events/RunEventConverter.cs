using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services
{
    public class RunEventConverter : IRunEventConverter

    {
        public IList<RunEvent> Convert(RunImport run)
            => new List<RunEvent>();
    }
}
