using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IWhitelistReader
    {
        IEnumerable<IPlayer> Read();

        bool Exists(string id);
    }
}
