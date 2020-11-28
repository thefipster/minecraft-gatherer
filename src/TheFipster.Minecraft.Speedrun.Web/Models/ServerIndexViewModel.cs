using TheFipster.Minecraft.Abstraction;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class ServerIndexViewModel
    {
        public ServerIndexViewModel() { }

        public IServerProperties ServerProperties { get; internal set; }
    }
}