using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Domain.Exceptions;

namespace TheFipster.Minecraft.Services
{
    public class ServerPropertiesReader : IServerPropertiesReader
    {
        private const string ServerPropertiesFilename = "server.properties";

        private readonly IConfigService _config;

        public ServerPropertiesReader(IConfigService config)
            => _config = config;

        public IServerProperties Read()
        {
            var file = find();
            var properties = parse(file);
            return properties;
        }

        private FileInfo find()
        {
            var matches = _config.ServerLocation.GetFiles(ServerPropertiesFilename);
            if (matches.Any())
                return matches.First();

            throw new ServerPropertiesNotFoundException(
                $"File {ServerPropertiesFilename} was not found in {_config.ServerLocation.FullName}");
        }

        private IServerProperties parse(FileInfo file)
        {
            var lines = File.ReadAllLines(file.FullName);
            return new ServerProperties
            {
                Difficulty = getString(lines, "difficulty"),
                GameMode = getString(lines, "gamemode"),
                IsConsoleBroadcastEnabled = getBool(lines, "broadcast-console-to-ops"),
                IsHardcore = getBool(lines, "hardcore"),
                IsOnlineMode = getBool(lines, "online-mode"),
                IsSpawnNpcEnabled = getBool(lines, "spawn-npcs"),
                IsSpawnAnimalsEnabled = getBool(lines, "spawn-animals"),
                IsSpawnMonstersEnabled = getBool(lines, "spawn-monsters"),
                LevelName = getString(lines, "level-name"),
                LevelSeed = getNullableLong(lines, "level-seed"),
                MessageOfTheDay = getString(lines, "motd"),
                SpawnProtection = getInt(lines, "spawn-protection"),
                ViewDistance = getInt(lines, "view-distance")
            };
        }

        private string getString(IEnumerable<string> lines, string propertyName)
        {
            var seedLine = lines.First(line => line.Contains($"{propertyName}="));
            var seedSplit = seedLine.Split("=");
            return seedSplit[1];
        }

        private int getInt(IEnumerable<string> lines, string propertyName)
        {
            var value = getString(lines, propertyName);
            return int.Parse(value);
        }

        private long? getNullableLong(IEnumerable<string> lines, string propertyName)
        {
            var value = getString(lines, propertyName);
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return long.Parse(value);
        }

        private bool getBool(IEnumerable<string> lines, string propertyName)
        {
            var value = getString(lines, propertyName);
            return bool.Parse(value);
        }
    }
}
