using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IServerPropertiesReader
    {
        IServerProperties Read();
    }
}
