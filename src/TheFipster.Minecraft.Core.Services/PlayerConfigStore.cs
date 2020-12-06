using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class PlayerConfigStore : IPlayerStore
    {
        private const string PlayerSectionName = "Players";

        private readonly IConfiguration _config;

        public PlayerConfigStore(IConfiguration config)
            => _config = config;

        public IPlayer GetPlayerById(string id)
            => GetPlayers().FirstOrDefault(x => x.Id == id);

        public IPlayer GetPlayerByName(string name)
            => GetPlayers().FirstOrDefault(x => x.Name == name);

        public IEnumerable<IPlayer> GetPlayers()
        {
            var players = new List<Player>();
            var section = _config.GetSection(PlayerSectionName);
            section.Bind(players);

            return players;
        }
    }
}
