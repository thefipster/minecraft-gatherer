using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IPlayerStore
    {
        IEnumerable<IPlayer> GetPlayers();

        IPlayer GetPlayerByName(string name);

        IPlayer GetPlayerById(string id);
    }
}
