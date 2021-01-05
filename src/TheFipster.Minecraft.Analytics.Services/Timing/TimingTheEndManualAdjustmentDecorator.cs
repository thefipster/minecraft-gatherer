using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;


namespace TheFipster.Minecraft.Analytics.Services
{
    public class TimingTheEndManualAdjustmentDecorator : ITimingAnalyzer
    {
        private readonly ITimingAnalyzer _component;
        private readonly IManualEndTimeAdjuster _adjuster;

        public TimingTheEndManualAdjustmentDecorator(ITimingAnalyzer component, IManualEndTimeAdjuster adjuster)
        {
            _component = component;
            _adjuster = adjuster;
        }

        public IEnumerable<RunEvent> ValidEvents
            => _component.ValidEvents;

        public TimingAnalytics Analyze(RunImport run)
        {
            var timings = _component.Analyze(run);
            timings = _adjuster.Adjust(timings);
            return timings;
        }
    }
}
