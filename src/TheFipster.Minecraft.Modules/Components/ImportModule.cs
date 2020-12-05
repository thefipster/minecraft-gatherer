using Microsoft.Extensions.Logging;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public class ImportModule : IImportRunModule
    {
        private readonly ILogLoader _logLoader;
        private readonly IDimensionLoader _dimensionLoader;
        private readonly IAchievementLoader _achievementLoader;
        private readonly IStatsLoader _statsLoader;
        private readonly IPlayerNbtLoader _playerNbtLoader;

        private readonly ILogger<ImportModule> _logger;

        public ImportModule(
            ILogLoader logLoader,
            IDimensionLoader dimensionLoader,
            IAchievementLoader achievementLoader,
            IStatsLoader statsLoader,
            IPlayerNbtLoader playerNbtLoader,
            ILogger<ImportModule> logger)
        {
            _logLoader = logLoader;
            _dimensionLoader = dimensionLoader;
            _achievementLoader = achievementLoader;
            _statsLoader = statsLoader;
            _playerNbtLoader = playerNbtLoader;
            _logger = logger;
        }

        public RunImport Import(WorldInfo world)
        {
            _logger.LogInformation($"Import started for world {world.Name}.");

            var run = new RunImport(world);
            run.Dimensions = _dimensionLoader.Load(world);
            run.Stats = _statsLoader.Load(world);
            run.Achievements = _achievementLoader.Load(world);
            run.Logs = _logLoader.Load(run);
            run.EndScreens = _playerNbtLoader.Load(world);

            _logger.LogInformation($"Import finished for world {world.Name}.");
            return run;
        }
    }
}
