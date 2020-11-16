using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class EventPlayerExtractor : IEventPlayerExtractor
    {
        public List<Player> Extract(IEnumerable<LogEvent> events)
        {
            var players = new List<Player>();

            foreach (var e in events)
                if (e.Player != null && !players.Any(x => x.Id == e.Player.Id))
                    players.Add(e.Player);

            return players;
        }
    }
}
