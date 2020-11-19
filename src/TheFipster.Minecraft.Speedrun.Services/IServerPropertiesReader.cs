using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IServerPropertiesReader
    {
        ServerProperties Read();
    }
}
