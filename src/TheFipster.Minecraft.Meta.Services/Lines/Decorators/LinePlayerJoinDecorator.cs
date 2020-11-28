using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Abstraction;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Lines.Decorators
{
    public class LinePlayerJoinDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IEnumerable<IPlayer> _players;

        public LinePlayerJoinDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            if (!line.Message.Contains("logged in with entity id"))
                return events;

            if (!findPlayer(line, out var player))
                return events;

            if (!findCoords(line, out var coords))
                return events;

            events.Add(new RunEvent(
                EventTypes.Join,
                line.Timestamp,
                coords,
                player.Id));

            return events;
        }

        private bool findCoords(LogLine line, out string coords)
        {
            var regEx = new Regex(@"\((.*?)\)");
            var matches = regEx.Matches(line.Message);

            if (matches.Any())
            {
                coords = matches
                    .First()
                    .Value
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty);

                return true;
            }

            coords = null;
            return false;
        }

        private bool findPlayer(LogLine line, out IPlayer player)
        {
            foreach (var p in _players)
            {
                if (line.Message.Contains(p.Name))
                {
                    player = p;
                    return true;
                }
            }

            player = null;
            return false;
        }
    }
}
