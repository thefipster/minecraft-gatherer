using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineAdvancementAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LineAdvancementAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<GameEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.Contains("advancement"))
                return events;

            var advancement = GameEvent.CreateAdvancement(line);

            advancement.Player = findPlayer(line);
            if (advancement.Player == null)
                return events;

            advancement.Data = findAdvancement(line);
            if (string.IsNullOrEmpty(advancement.Data))
                return events;

            events.Add(advancement);
            return events;
        }

        private string findAdvancement(LogLine line)
        {
            var regEx = new Regex(@"\[(.*?)\]");
            var matches = regEx.Matches(line.Message);

            if (matches.Any())
            {
                var title = matches
                    .First()
                    .Value
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                return title;
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
