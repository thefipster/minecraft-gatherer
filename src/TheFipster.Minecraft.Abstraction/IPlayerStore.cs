using System.Collections.Generic;
using TheFipster.Minecraft.Abstraction;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IPlayerStore
    {
        IEnumerable<IPlayer> GetPlayers();

        IPlayer GetPlayerByName(string name);

        IPlayer GetPlayerById(string id);
    }
}
