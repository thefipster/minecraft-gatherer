using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services.Lines
{
    public class LineEventConverter : ILogLineEventConverter
    {
        public ICollection<RunEvent> Convert(LogLine line)
            => new List<RunEvent>();
    }
}
