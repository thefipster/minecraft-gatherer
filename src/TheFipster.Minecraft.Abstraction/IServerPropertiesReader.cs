using TheFipster.Minecraft.Abstraction;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IServerPropertiesReader
    {
        IServerProperties Read();
    }
}
