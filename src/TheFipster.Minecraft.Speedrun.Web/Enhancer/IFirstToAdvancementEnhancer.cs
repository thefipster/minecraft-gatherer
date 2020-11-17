using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Web.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public interface IFirstToAdvancementEnhancer
    {
        IEnumerable<FirstEvent> Enhance(RunInfo run);

    }
}
