using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IEventTimingExtractor
    {
        Timings Extract(RunInfo run);

    }
}
