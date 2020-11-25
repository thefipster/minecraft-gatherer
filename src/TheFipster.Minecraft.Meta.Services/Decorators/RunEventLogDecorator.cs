using System.Collections.Generic;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services.Decorators
{
    public class RunEventLogDecorator : IRunEventConverter
    {
        private readonly IRunEventConverter _component;
        private readonly IPlayerStore _playerStore;

        public RunEventLogDecorator(IRunEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public IList<RunEvent> Convert(RunImport run)
        {
            var events = _component.Convert(run);

            // perform magic here

            return events;
        }
    }
}
