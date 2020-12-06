using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class PlayerEventExtender : IPlayerEventExtender
    {
        public Dictionary<string, IEnumerable<RunEvent>> Extend(RunImport run)
        {
            var players = new Dictionary<string, IEnumerable<RunEvent>>();

            foreach (var playerId in run.PlayerIds)
            {
                var playerEvents = new List<RunEvent>();
                var events = run.Events.Where(x => x.PlayerId == playerId);
                var eventNames = events.Select(x => x.Value).Distinct();

                foreach (var eventName in eventNames)
                {
                    var quickest = events.Where(x => x.Value == eventName).OrderBy(x => x.Timestamp).First();
                    var gameEvent = new RunEvent(quickest.Type, quickest.Timestamp, quickest.Value, quickest.PlayerId);
                    playerEvents.Add(gameEvent);
                }

                players.Add(playerId, playerEvents);
            }

            return players;
        }
    }
}
