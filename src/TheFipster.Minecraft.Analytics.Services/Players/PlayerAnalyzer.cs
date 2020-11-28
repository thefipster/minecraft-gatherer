using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services.Players
{
    public class PlayerAnalyzer : IPlayerAnalyzer
    {
        private readonly IPlayerStore _playerStore;

        public PlayerAnalyzer(IPlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public ICollection<PlayerAnalytics> Analyze(RunImport import)
        {
            var players = new List<PlayerAnalytics>();

            foreach (var playerId in import.PlayerIds)
            {
                var player = _playerStore.GetPlayerById(playerId);
                players.Add(new PlayerAnalytics(player));
            }

            return players;
        }
    }
}
