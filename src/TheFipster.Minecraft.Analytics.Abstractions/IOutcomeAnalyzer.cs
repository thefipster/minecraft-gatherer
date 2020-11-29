using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IOutcomeAnalyzer
    {
        Outcomes Analyze(RunImport run);
    }
}
