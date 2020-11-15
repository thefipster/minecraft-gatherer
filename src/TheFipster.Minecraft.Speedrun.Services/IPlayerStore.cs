using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IPlayerStore
    {
        IEnumerable<Player> GetPlayers();

        Player GetPlayerByName(string name);

        Player GetPlayerById(string id);
    }
}
