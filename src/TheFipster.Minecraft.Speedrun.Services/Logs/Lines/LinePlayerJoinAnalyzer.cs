using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LinePlayerJoinAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LinePlayerJoinAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<LogEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.Contains("logged in with entity id"))
                return events;

            var joined = LogEvent.CreateJoined(line);

            joined.Player = findPlayer(line);
            if (joined.Player == null)
                return events;

            joined.Data = findCoords(line);

            events.Add(joined);

            return events;
        }

        private string findCoords(LogLine line)
        {
            var regEx = new Regex(@"\((.*?)\)");
            var matches = regEx.Matches(line.Message);

            if (matches.Any())
            {
                var coords = matches
                    .First()
                    .Value
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty);

                return coords;
            }

            return null;
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
