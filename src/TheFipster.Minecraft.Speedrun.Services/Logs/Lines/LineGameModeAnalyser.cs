using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineGameModeAnalyser : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LineGameModeAnalyser(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<GameEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.ToLower().Contains("set own game mode to"))
                return events;

            var player = extractPlayer(line);
            var gameMode = extractGameMode(line);
            var gameEvent = GameEvent.CreateGameMode(line, player, gameMode);

            events.Add(gameEvent);
            return events;
        }

        private string extractGameMode(LogLine line)
        {
            if (line.Message.ToLower().Contains("spectator mode"))
                return "Spectator";

            if (line.Message.ToLower().Contains("creative mode"))
                return "Creative";

            if (line.Message.ToLower().Contains("survival mode"))
                return "Survival";

            if (line.Message.ToLower().Contains("adventure mode"))
                return "Adventure";

            return string.Empty;
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
