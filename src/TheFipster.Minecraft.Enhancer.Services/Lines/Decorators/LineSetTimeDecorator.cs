using System.Collections.Generic;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Lines.Decorators
{
    public class LineSetTimeDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IEnumerable<IPlayer> _players;

        public LineSetTimeDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            if (!line.Message.Contains("Set the time"))
                return events;

            if (!findPlayer(line, out var player))
                return events;

            var setTime = findTime(line);

            events.Add(new RunEvent(
                EventTypes.SetTime,
                line.Timestamp,
                setTime,
                player.Id));

            return events;
        }

        private string findTime(LogLine line)
        {
            var message = line.Message.Replace("]", string.Empty);
            var search = "Set the time to ";
            var index = message.IndexOf(search);
            var time = message.Substring(index + search.Length);

            return time;
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
