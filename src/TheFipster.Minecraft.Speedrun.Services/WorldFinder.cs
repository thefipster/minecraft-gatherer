using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class WorldFinder : IWorldFinder
    {
        private readonly IConfigService _config;
        private readonly ILogger<WorldFinder> _logger;

        public WorldFinder(IConfigService config, ILogger<WorldFinder> logger)
        {
            _config = config;
            _logger = logger;
        }

        public IEnumerable<WorldInfo> Find()
        {
            var candidates = _config.ServerLocation.GetDirectories("world-*");

            foreach (var candidate in candidates)
            {
                var worldDir = candidate.Name;
                var splits = worldDir.Split('-');

                if (validateWorld(splits))
                    yield return handleWorld(candidate, splits);
            }
        }

        private WorldInfo handleWorld(DirectoryInfo candidate, string[] splits)
        {
            var dirSize = candidate
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Sum(file => file.Length);

            var unixEpoch = int.Parse(splits[1]);
            var createdDate = DateTimeOffset.FromUnixTimeSeconds(unixEpoch);

            return new WorldInfo
            {
                CreatedOn = createdDate.DateTime,
                Path = candidate.FullName,
                SizeInBytes = dirSize
            };
        }

        private bool validateWorld(string[] splits)
        {
            if (splits.Length < 2)
            {
                _logger.LogInformation("Omitting world during find because it doesn't match naming pattern. {worldDir} has not two dashes.");
                return false;
            }

            if (!int.TryParse(splits[1], out var unixEpoch))
            {
                _logger.LogInformation("Omitting world during find because it doesn't match naming pattern. {worldDir} has not a valid unix epoch after the first dash.");
                return false;
            }

            return true;
        }
    }
}
