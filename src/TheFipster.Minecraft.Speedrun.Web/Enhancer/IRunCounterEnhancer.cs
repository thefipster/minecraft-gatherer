using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Enhancer
{
    public interface IRunCounterEnhancer
    {
        RunCounts Enhance();
    }
}
