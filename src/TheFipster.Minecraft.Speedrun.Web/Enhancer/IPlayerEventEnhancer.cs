using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public interface IPlayerEventEnhancer
    {
        Dictionary<string, IEnumerable<GameEvent>> Enhance(RunInfo run);
    }
}
