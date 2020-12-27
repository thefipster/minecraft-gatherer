using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class ServerIndexViewModel
    {
        public ServerIndexViewModel() { }

        public ServerProperties ServerProperties { get; internal set; }
        public IEnumerable<IPlayer> WhitelistedPlayers { get; internal set; }
        public IEnumerable<IPlayer> Operators { get; internal set; }
    }
}