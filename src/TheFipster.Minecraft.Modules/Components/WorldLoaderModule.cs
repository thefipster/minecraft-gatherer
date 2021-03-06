﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Core.Domain.Exceptions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public class WorldLoaderModule : IWorldLoaderModule
    {
        private readonly IWorldSearcher _worldFinder;
        private readonly IWorldLoader _worldLoader;
        private readonly IImportReader _importReader;
        private readonly ServerProperties _serverProperties;
        private readonly ILogger<WorldLoaderModule> _logger;

        public WorldLoaderModule(
            IWorldSearcher worldFinder,
            IWorldLoader worldLoader,
            IImportReader importReader,
            IServerPropertiesReader serverPropReader,
            ILogger<WorldLoaderModule> logger)
        {
            _worldFinder = worldFinder;
            _worldLoader = worldLoader;
            _importReader = importReader;
            _logger = logger;

            try
            {
                _serverProperties = serverPropReader.Read();
            }
            catch (ServerPropertiesNotFoundException ex)
            {
                _logger.LogWarning(ex, "Candidate Check is unable to read server.properties. Skipping last world on import assuming it is the active one.");
            }
        }

        public IEnumerable<WorldInfo> Load(bool overwrite)
        {
            var worlds = new List<WorldInfo>();
            var candidates = _worldFinder.Find();

            if (_serverProperties == null)
                candidates = candidates.OrderBy(x => x.Name).Take(candidates.Count() - 1);
            else
                candidates = candidates.Where(x => x.Name != _serverProperties.LevelName).OrderBy(x => x.Name);

            foreach (var candiate in candidates)
            {
                if (!overwrite && _importReader.Exists(candiate.Name))
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
