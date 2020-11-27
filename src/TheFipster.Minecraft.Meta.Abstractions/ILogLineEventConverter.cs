using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Enhancer.Domain;

namespace TheFipster.Minecraft.Enhancer.Abstractions
{
    public interface ILogLineEventConverter
    {
        ICollection<RunEvent> Convert(LogLine line);
    }
}
