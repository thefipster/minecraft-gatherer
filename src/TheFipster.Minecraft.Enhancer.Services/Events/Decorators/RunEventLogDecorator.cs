using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Decorators
{
    public class RunEventLogDecorator : IRunEventConverter
    {
        private readonly IRunEventConverter _component;
        private readonly ILogLineEventConverter _lineConverter;

        public RunEventLogDecorator(IRunEventConverter component, ILogLineEventConverter lineConverter)
        {
            _component = component;
            _lineConverter = lineConverter;
        }

        public IList<RunEvent> Convert(RunImport run)
        {
            var events = _component.Convert(run);

            foreach (var line in run.Logs)
            {
                var lineEvents = _lineConverter.Convert(line);
                foreach (var lineEvent in lineEvents)
                    if (!events.Contains(lineEvent))
                        events.Add(lineEvent);
            }

            return events;
        }
    }
}
