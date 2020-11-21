using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineDeathAnalyzer : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;
        private IEnumerable<Player> _players;

        public LineDeathAnalyzer(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<GameEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);
            _players = _playerStore.GetPlayers();
            var player = extractPlayer(line);
            GameEvent gameEvent = null;

            if (gameEvent == null && line.Message.ToLower().Contains("slain by piglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Piglin");

            if (gameEvent == null && line.Message.ToLower().Contains("shot by piglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Piglin");

            if (gameEvent == null && line.Message.ToLower().Contains("escape piglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Piglin");


            if (gameEvent == null && line.Message.ToLower().Contains("fireballed by ghast"))
                gameEvent = GameEvent.CreateDeath(line, player, "Ghast");

            if (gameEvent == null && line.Message.ToLower().Contains("escape ghast"))
                gameEvent = GameEvent.CreateDeath(line, player, "Ghast");


            if (gameEvent == null && line.Message.ToLower().Contains("escape blaze"))
                gameEvent = GameEvent.CreateDeath(line, player, "Blaze");

            if (gameEvent == null && line.Message.ToLower().Contains("fireballed by blaze"))
                gameEvent = GameEvent.CreateDeath(line, player, "Blaze");

            if (gameEvent == null && line.Message.ToLower().Contains("fighting blaze"))
                gameEvent = GameEvent.CreateDeath(line, player, "Blaze");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by hoglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Hoglin");

            if (gameEvent == null && line.Message.ToLower().Contains("escape hoglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Hoglin");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by zombified piglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Zombified Piglin");


            if (gameEvent == null && line.Message.ToLower().Contains("escape zombified piglin"))
                gameEvent = GameEvent.CreateDeath(line, player, "Zombified Piglin");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by enderman"))
                gameEvent = GameEvent.CreateDeath(line, player, "Enderman");

            if (gameEvent == null && line.Message.ToLower().Contains("escape enderman"))
                gameEvent = GameEvent.CreateDeath(line, player, "Enderman");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by zombie"))
                gameEvent = GameEvent.CreateDeath(line, player, "Zombie");

            if (gameEvent == null && line.Message.ToLower().Contains("escape zombie"))
                gameEvent = GameEvent.CreateDeath(line, player, "Zombie");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by silverfish"))
                gameEvent = GameEvent.CreateDeath(line, player, "Silverfish");

            if (gameEvent == null && line.Message.ToLower().Contains("escape silverfish"))
                gameEvent = GameEvent.CreateDeath(line, player, "Silverfish");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by ender dragon"))
                gameEvent = GameEvent.CreateDeath(line, player, "Ender Dragon");

            if (gameEvent == null && line.Message.ToLower().Contains("killed by ender dragon"))
                gameEvent = GameEvent.CreateDeath(line, player, "Ender Dragon");

            if (gameEvent == null && line.Message.ToLower().Contains("escape ender dragon"))
                gameEvent = GameEvent.CreateDeath(line, player, "Ender Dragon");


            if (gameEvent == null && line.Message.ToLower().Contains("slain by wither skeleton"))
                gameEvent = GameEvent.CreateDeath(line, player, "Wither Skeleton");

            if (gameEvent == null && line.Message.ToLower().Contains("escape wither skeleton"))
                gameEvent = GameEvent.CreateDeath(line, player, "Wither Skeleton");

            if (gameEvent == null && line.Message.ToLower().Contains("withered away"))
                gameEvent = GameEvent.CreateDeath(line, player, "Wither Skeleton");


            if (gameEvent == null && line.Message.ToLower().Contains("shot by skeleton"))
                gameEvent = GameEvent.CreateDeath(line, player, "Skeleton");

            if (gameEvent == null && line.Message.ToLower().Contains("escape skeleton"))
                gameEvent = GameEvent.CreateDeath(line, player, "Skeleton");


            if (gameEvent == null && line.Message.ToLower().Contains("blown up by creeper"))
                gameEvent = GameEvent.CreateDeath(line, player, "Creeper");

            if (gameEvent == null && line.Message.ToLower().Contains("escape creeper"))
                gameEvent = GameEvent.CreateDeath(line, player, "Creeper");


            if (gameEvent == null && line.Message.ToLower().Contains("killed by [intentional game design]"))
                gameEvent = GameEvent.CreateDeath(line, player, "Game Design");

            if (gameEvent == null && (line.Message.ToLower().Contains(" killed by ") || line.Message.ToLower().Contains(" slain by ") || line.Message.ToLower().Contains(" escape ")))
            {
                foreach (var storedPlayer in _players)
                {
                    if (!line.Message.Split(" ").Skip(3).Any(x => x.Contains(storedPlayer.Name)))
                        continue;

                    gameEvent = GameEvent.CreateDeath(line, player, storedPlayer.Name);
                    break;
                }
            }

            if (gameEvent == null && line.Message.ToLower().Contains("fell from a high place"))
                gameEvent = GameEvent.CreateDeath(line, player, "Gravity");

            if (gameEvent == null && line.Message.ToLower().Contains("burned to death"))
                gameEvent = GameEvent.CreateDeath(line, player, "Fire");

            if (gameEvent == null && line.Message.ToLower().Contains("went up in flames"))
                gameEvent = GameEvent.CreateDeath(line, player, "Fire");

            if (gameEvent == null && line.Message.ToLower().Contains("tried to swim in lava"))
                gameEvent = GameEvent.CreateDeath(line, player, "Lava");

            if (gameEvent == null && line.Message.ToLower().Contains("fell out of the world"))
                gameEvent = GameEvent.CreateDeath(line, player, "Void");

            if (gameEvent != null)
                events.Add(gameEvent);

            return events;
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
