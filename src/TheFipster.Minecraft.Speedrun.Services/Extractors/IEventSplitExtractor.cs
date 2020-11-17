using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IEventSplitExtractor
    {
        List<Split> Extract(IEnumerable<LogEvent> events);

    }
}
