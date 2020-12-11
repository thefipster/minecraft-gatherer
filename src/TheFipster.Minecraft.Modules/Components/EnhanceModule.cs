using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules.Components
{
    public class EnhanceModule : IEnhanceModule
    {
        private readonly IRunEventConverter _runEventConverter;
        private readonly IPlayerFinder _playerFinder;
        private readonly INbtLevelConverter _nbtLevel;
        private readonly INbtPlayerConverter _nbtPlayer;

        public EnhanceModule(
            IRunEventConverter runEventConverter,
            IPlayerFinder playerFinder,
            INbtLevelConverter nbtLevel,
            INbtPlayerConverter nbtPlayer)
        {
            _runEventConverter = runEventConverter;
            _playerFinder = playerFinder;
            _nbtLevel = nbtLevel;
            _nbtPlayer = nbtPlayer;
        }

        public RunImport Enhance(RunImport run)
        {
            run.Events = _runEventConverter.Convert(run);
            run.PlayerIds = _playerFinder.Find(run);
            run.LevelNbt = _nbtLevel.Convert(run);
            run.PlayerNbts = _nbtPlayer.Convert(run);

            return run;
        }
    }
}
