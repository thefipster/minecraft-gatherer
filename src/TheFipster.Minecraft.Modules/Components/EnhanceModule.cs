using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules.Components
{
    public class EnhanceModule : IEnhanceModule
    {
        private readonly IRunEventConverter _runEventConverter;
        private readonly IPlayerFinder _playerFinder;

        public EnhanceModule(
            IRunEventConverter runEventConverter,
            IPlayerFinder playerFinder)
        {
            _runEventConverter = runEventConverter;
            _playerFinder = playerFinder;
        }

        public RunImport Enhance(RunImport run)
        {
            run.Events = _runEventConverter.Convert(run);
            run.PlayerIds = _playerFinder.Find(run);

            return run;
        }
    }
}
