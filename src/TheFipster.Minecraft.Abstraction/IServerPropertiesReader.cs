using TheFipster.Minecraft.Domain;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IServerPropertiesReader
    {
        ServerProperties Read();
    }
}
