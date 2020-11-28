using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Abstractions;

namespace TheFipster.Minecraft.Enhancer.Services.Lines.Decorators
{
    public class LineGameModeDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IPlayerStore _playerStore;

        public LineGameModeDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            if (!line.Message.ToLower().Contains("set own game mode to"))
                return events;

            var player = extractPlayer(line);
            var gameMode = extractGameMode(line);

            events.Add(new RunEvent(EventTypes.GameMode, line.Timestamp, gameMode, player.Id));
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

        private IPlayer extractPlayer(LogLine line)
        {
            foreach (var player in _playerStore.GetPlayers())
                if (line.Message.Split(" ").Take(3).Any(x => x.Contains(player.Name)))
                    return player;

            return null;
        }
    }
}
