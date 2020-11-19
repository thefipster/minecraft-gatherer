using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LinePlayerLeaveAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LinePlayerLeaveAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<GameEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.Contains("lost connection"))
                return events;

            var left = GameEvent.CreateLeft(line);

            left.Player = findPlayer(line);
            if (left.Player == null)
                return events;

            left.Data = findReason(line);

            events.Add(left);

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

        private Player findPlayer(LogLine line)
        {
            foreach (var player in _playerStore.GetPlayers())
                if (line.Message.Contains(player.Name))
                    return player;

            return null;
        }
    }
}
