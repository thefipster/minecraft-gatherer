using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IValidityChecker
    {
        ValidityResult Check(RunInfo run);
    }
}
