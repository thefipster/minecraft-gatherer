using TheFipster.Minecraft.Abstractions;

namespace TheFipster.Minecraft.Abstractions
{
    public interface IServerPropertiesReader
    {
        IServerProperties Read();
    }
}
