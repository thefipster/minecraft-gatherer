using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IServerPropertiesReader
    {
        ServerProperties Read();
    }
}
