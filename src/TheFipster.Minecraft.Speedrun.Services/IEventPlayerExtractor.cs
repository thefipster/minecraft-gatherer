using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IEventPlayerExtractor
    {
        List<Player> Extract(IEnumerable<LogEvent> events);
    }
}
