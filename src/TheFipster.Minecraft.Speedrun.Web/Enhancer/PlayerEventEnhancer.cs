using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public class PlayerEventEnhancer : IPlayerEventEnhancer
    {
        public Dictionary<string, IEnumerable<GameEvent>> Enhance(RunInfo run)
        {
            var players = new Dictionary<string, IEnumerable<GameEvent>>();

            foreach (var player in run.Players)
            {
                var playerEvents = new List<GameEvent>();
                var events = run.Events.Where(x => x.Player != null && x.Player.Id == player.Id);
                var eventNames = events.Select(x => x.Data).Distinct();

                foreach (var eventName in eventNames)
                {
                    var quickest = events.Where(x => x.Data == eventName).Select(x => x.Timestamp).Min(x => x);
                    var gameEvent = GameEvent.CreateBasic(player, quickest, eventName);
                    playerEvents.Add(gameEvent);
                }

                players.Add(player.Id, playerEvents);
            }

            return players;
        }
    }
}
