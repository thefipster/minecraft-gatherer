using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineTeleportAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LineTeleportAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<GameEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            var teleportIndex = line.Message.ToLower().IndexOf("teleported");
            var toIndex = line.Message.ToLower().IndexOf("to");

            if (teleportIndex >= 0 && toIndex > teleportIndex)
            {
                var player = extractPlayer(line);
                var destination = extractDestination(line);
                var gameEvent = GameEvent.CreateTeleport(line, player, destination);
                events.Add(gameEvent);
            }

            return events;
        }

        private string extractDestination(LogLine line)
        {
            var toIndex = line.Message.ToLower().LastIndexOf("to");
            var destination = line.Message.Substring(toIndex + 2).Replace("]", string.Empty).Trim();
            return destination;
        }

        private Player extractPlayer(LogLine line)
        {
            foreach (var player in _playerStore.GetPlayers())
                if (line.Message.Split(" ").Take(3).Any(x => x.Contains(player.Name)))
                    return player;

            return null;
        }
    }
}
