using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class ConfigService : IConfigService
    {
        private const string MinecraftLocationKey = "MinecraftLocation";
        private const string TempLocationKey = "TempLocation";
        private const string DataLocationKey = "DataLocation";
        private const string LogLocationKey = "LogLocation";
        private const string InitialRunIndexKey = "InitialRunIndex";

        private readonly IConfiguration _config;

        public ConfigService(IConfiguration config)
        {
            _config = config;

            ServerLocation = checkExistance(MinecraftLocationKey);
            LogLocation = checkExistance(LogLocationKey);
            TempLocation = ensureExistance(TempLocationKey);
            DataLocation = ensureExistance(DataLocationKey);
            InitialRunIndex = ensureInt(InitialRunIndexKey);

        }

        private int ensureInt(string configKey)
        {
            if (int.TryParse(_config[configKey], out var runIndex))
                return runIndex;

            throw new Exception($"AppSetting {configKey} was not a valid integer.");
        }

        public DirectoryInfo ServerLocation { get; }
        public DirectoryInfo LogLocation { get; }
        public DirectoryInfo TempLocation { get; }
        public DirectoryInfo DataLocation { get; }
        public int InitialRunIndex { get; }

        private DirectoryInfo checkExistance(string configKey)
        {
            var path = _config[configKey];
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"{configKey} directory doesn't exist.");

            return dir;
        }

        private DirectoryInfo ensureExistance(string configKey)
        {
            var location = _config[configKey];
            var directory = new DirectoryInfo(location);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
