using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Lines
{
    public class LineEventConverter : ILogLineEventConverter
    {
        public ICollection<RunEvent> Convert(LogLine line)
            => new List<RunEvent>();
    }
}
