using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IOutcomeChecker
    {
        OutcomeResult Check(RunInfo run);
    }
}
