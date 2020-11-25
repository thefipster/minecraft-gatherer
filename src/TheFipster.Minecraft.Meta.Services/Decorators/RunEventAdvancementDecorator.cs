using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services.Decorators
{
    public class RunEventAdvancementDecorator : IRunEventConverter
    {
        private readonly IRunEventConverter _component;

        public RunEventAdvancementDecorator(IRunEventConverter component)
        {
            _component = component;
        }

        public IList<RunEvent> Convert(RunImport run)
        {
            var events = _component.Convert(run);

            // perform magic here

            return events;
        }
    }
}
