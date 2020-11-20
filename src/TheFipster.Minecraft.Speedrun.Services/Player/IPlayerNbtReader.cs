using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IPlayerNbtReader
    {
        Dictionary<string, bool> Read(WorldInfo world);
    }
}
