using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface ITimingAnalyzer
    {
        TimingAnalytics Analyse(RunImport run);
        IEnumerable<RunEvent> ValidEvents { get; }
    }
}
