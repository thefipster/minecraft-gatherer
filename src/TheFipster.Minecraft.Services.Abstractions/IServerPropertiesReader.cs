using TheFipster.Minecraft.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IServerPropertiesReader
    {
        ServerProperties Read();
    }
}
