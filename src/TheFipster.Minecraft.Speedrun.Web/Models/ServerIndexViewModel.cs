using TheFipster.Minecraft.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class ServerIndexViewModel
    {
        public ServerIndexViewModel() { }

        public IServerProperties ServerProperties { get; internal set; }
    }
}