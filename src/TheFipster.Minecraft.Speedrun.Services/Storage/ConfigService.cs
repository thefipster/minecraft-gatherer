using Microsoft.Extensions.Configuration;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class ConfigService : IConfigService
    {
        private const string MinecraftLocationKey = "MinecraftLocation";
        private const string TempLocationKey = "TempLocation";
        private const string DataLocationKey = "DataLocation";

        private readonly IConfiguration _config;

        public ConfigService(IConfiguration config)
        {
            _config = config;

            ServerLocation = checkExistance(MinecraftLocationKey);
            LogLocation = setLogDir(ServerLocation);
            TempLocation = ensureExistance(TempLocationKey);
            DataLocation = ensureExistance(DataLocationKey);
        }

        public DirectoryInfo ServerLocation { get; }
        public DirectoryInfo LogLocation { get; }
        public DirectoryInfo TempLocation { get; }
        public DirectoryInfo DataLocation { get; }

        private DirectoryInfo checkExistance(string configKey)
        {
            var serverRoot = _config[MinecraftLocationKey];
            var workdir = new DirectoryInfo(serverRoot);

            if (!workdir.Exists)
                throw new DirectoryNotFoundException($"{configKey} directory doesn't exist.");

            return workdir;
        }

        private DirectoryInfo ensureExistance(string configKey)
        {
            var location = _config[configKey];
            var directory = new DirectoryInfo(location);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }

        private DirectoryInfo setLogDir(DirectoryInfo serverLocation)
        {
            var path = Path.Combine(serverLocation.FullName, "logs");
            return new DirectoryInfo(path);
        }
    }
}
