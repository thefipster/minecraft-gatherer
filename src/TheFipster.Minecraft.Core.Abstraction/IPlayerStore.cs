﻿using System.Collections.Generic;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IPlayerStore
    {
        IEnumerable<IPlayer> GetPlayers();

        IPlayer GetPlayerByName(string name);

        IPlayer GetPlayerById(string id);
    }
}
