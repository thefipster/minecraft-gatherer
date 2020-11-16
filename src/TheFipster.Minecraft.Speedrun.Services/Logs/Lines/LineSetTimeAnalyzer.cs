using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineSetTimeAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LineSetTimeAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<LogEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.Contains("Set the time"))
                return events;

            var advancement = LogEvent.CreateSetTime(line);

            advancement.Player = findPlayer(line);
            if (advancement.Player == null)
                return events;

            advancement.Data = findTime(line);

            events.Add(advancement);
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

        private Player findPlayer(LogLine line)
        {
            foreach (var player in _playerStore.GetPlayers())
                if (line.Message.Contains(player.Name))
                    return player;

            return null;
        }
    }
}
