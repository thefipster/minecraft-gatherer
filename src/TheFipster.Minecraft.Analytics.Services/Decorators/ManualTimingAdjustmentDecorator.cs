using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Analytics.Services
{
    public class ManualTimingAdjustmentDecorator : IManualsWriter
    {
        private readonly IManualsWriter _component;
        private readonly IManualEndTimeAdjuster _adjuster;

        public ManualTimingAdjustmentDecorator(IManualsWriter component, IManualEndTimeAdjuster adjuster)
        {
            _component = component;
            _adjuster = adjuster;
        }

        public bool Exists(string name)
            => _component.Exists(name);

        public void Upsert(RunManuals manuals)
        {
            _component.Upsert(manuals);
            _adjuster.Adjust(manuals);
        }
    }
}
