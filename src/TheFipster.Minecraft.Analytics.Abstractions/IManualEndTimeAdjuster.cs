using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IManualEndTimeAdjuster
    {
        void Adjust(RunManuals manuals);
        TimingAnalytics Adjust(TimingAnalytics timings);
    }
}
