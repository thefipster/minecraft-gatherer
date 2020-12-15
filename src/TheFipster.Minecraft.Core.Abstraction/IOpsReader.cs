using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IOpsReader
    {
        IEnumerable<IPlayer> Read();
        bool Exists(string id);
    }
}
