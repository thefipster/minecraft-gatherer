using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class WorldLoaderModule : IWorldLoaderModule
    {
        private readonly IWorldFinder _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly IRunStore _runStore;
        private readonly ServerProperties _serverProperties;
        private readonly ILogger<WorldLoaderModule> _logger;

        public WorldLoaderModule(IWorldFinder worldFinder, IWorldLoader worldLoader, IRunStore runStore, IServerPropertiesReader serverPropReader, ILogger<WorldLoaderModule> logger)
        {
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _runStore = runStore;
            _serverProperties = serverPropReader.Read();
            _logger = logger;
        }

        public IEnumerable<WorldInfo> Load(bool overwrite)
        {
            var worlds = new List<WorldInfo>();
            var candidates = _worldFinder.Find();

            foreach (var candiate in candidates)
            {
                if (candiate.Name == _serverProperties.LevelName)
                {
                    _logger.LogInformation($"Candidate Check: Skipping world {candiate.Name} because it is currently active.");
                    continue;
                }

                if (!overwrite && _runStore.Exists(candiate.Name))
                {
                    _logger.LogDebug($"Candidate Check: Skipping world {candiate.Name} because it was already imported.");
                    continue;
                }

                if (tryLoadWorldFolder(candiate, out var world))
                {
                    _logger.LogDebug($"Candidate Check: Loading world {candiate.Name}.");
                    worlds.Add(world);
                }
            }

            _logger.LogDebug($"Candidate Check: Completed. Returning {worlds.Count()} runs.");
            return worlds.OrderBy(x => x.CreatedOn);
        }

        private bool tryLoadWorldFolder(DirectoryInfo candiate, out WorldInfo world)
        {
            world = new WorldInfo();

            try
            {
                world = _worldLoader.Load(candiate);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Couldn't load world.");
                return false;
            }
        }
    }
}
