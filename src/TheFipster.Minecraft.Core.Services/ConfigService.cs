using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class ConfigService : IConfigService
    {
        private const string LocationSection = "Locations";
        private const string InitialRunIndexKey = "InitialRunIndex";

        private readonly IConfiguration _config;

        private ServerPaths _paths;

        public ConfigService(IConfiguration config)
        {
            _config = config;
            _paths = new ServerPaths();
            _config.GetSection(LocationSection).Bind(_paths);

            ServerLocation = checkExistance(_paths.Server);
            LogLocation = checkExistance(_paths.Logs);
            TempLocation = ensureExistance(_paths.Temp);
            DataLocation = ensureExistance(_paths.Data);
            InitialRunIndex = ensureInt(InitialRunIndexKey);
        }

        public DirectoryInfo ServerLocation { get; }
        public DirectoryInfo LogLocation { get; }
        public DirectoryInfo TempLocation { get; }
        public DirectoryInfo DataLocation { get; }

        public int InitialRunIndex { get; }

        private DirectoryInfo checkExistance(string path)
        {
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"{path} doesn't exist.");

            return dir;
        }

        private DirectoryInfo ensureExistance(string path)
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }

        private int ensureInt(string configKey)
        {
            if (int.TryParse(_config[configKey], out var runIndex))
                return runIndex;

            throw new Exception($"AppSetting {configKey} was not a valid integer.");
        }
    }
}
