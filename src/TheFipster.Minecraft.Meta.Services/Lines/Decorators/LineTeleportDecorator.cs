using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services.Lines.Decorators
{
    public class LineTeleportDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IEnumerable<Player> _players;

        public LineTeleportDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            var teleportIndex = line.Message.ToLower().IndexOf("teleported");
            var toIndex = line.Message.ToLower().IndexOf("to");

            if (teleportIndex >= 0 && toIndex > teleportIndex)
            {
                var player = extractPlayer(line);
                var destination = extractDestination(line);

                events.Add(new RunEvent(
                    EventTypes.Teleport,
                    line.Timestamp,
                    destination,
                    player.Id));
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
            foreach (var player in _players)
                if (line.Message.Split(" ").Take(3).Any(x => x.Contains(player.Name)))
                    return player;

            return null;
        }
    }
}
