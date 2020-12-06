using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services.Abstractions
{
    public interface IPlayerStore
    {
        IEnumerable<Player> GetPlayers();

        Player GetPlayerByName(string name);

        Player GetPlayerById(string id);
    }
}
