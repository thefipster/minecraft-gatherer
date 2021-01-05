using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IOutcomeStatsExtender
    {
        OutcomeHistogram Extend();
        OutcomeHistogram Extend(Period period);
    }
}
