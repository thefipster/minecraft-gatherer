using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class StatsPlayerExtractor : IStatsPlayerExtractor
    {
        private readonly IPlayerStore _playerStore;

        public StatsPlayerExtractor(IPlayerStore playerStore)
            => _playerStore = playerStore;

        public IEnumerable<Player> Extract(IEnumerable<PlayerStats> stats)
        {
            var result = new List<Player>();
            var storedPlayers = _playerStore.GetPlayers();

            foreach (var stat in stats)
                if (storedPlayers.Any(x => x.Id == stat.PlayerId))
                    result.Add(storedPlayers.First(x => x.Id == stat.PlayerId));

            return result;
        }
    }
}
