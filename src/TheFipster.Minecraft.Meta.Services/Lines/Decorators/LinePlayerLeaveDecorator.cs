using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services.Lines.Decorators
{
    public class LinePlayerLeaveDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IEnumerable<Player> _players;

        public LinePlayerLeaveDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            if (!line.Message.Contains("lost connection"))
                return events;

            if (!findPlayer(line, out var player))
                return events;

            var reason = findReason(line);

            events.Add(new RunEvent(
                EventTypes.Leave,
                line.Timestamp,
                reason,
                player.Id));

            return events;
        }

        private string findReason(LogLine line)
        {
            var message = line.Message;
            var search = ": ";
            var index = message.IndexOf(search);
            var reason = message.Substring(index + search.Length);

            return reason;
        }

        private bool findPlayer(LogLine line, out Player player)
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
