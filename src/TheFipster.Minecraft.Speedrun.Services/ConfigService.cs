using Microsoft.Extensions.Configuration;
using System.IO;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class ConfigService : IConfigService
    {
        private const string MinecraftLocationKey = "MinecraftLocation";
        private const string TempLocationKey = "TempLocation";

        private readonly IConfiguration _config;

        public ConfigService(IConfiguration config)
        {
            _config = config;

            ServerLocation = guardWorkDir();
            LogLocation = setLogDir();
            TempLocation = guardTempDir();
        }

        public DirectoryInfo ServerLocation { get; }
        public DirectoryInfo LogLocation { get; }
        public DirectoryInfo TempLocation { get; }

        private DirectoryInfo guardWorkDir()
        {
            var serverRoot = _config[MinecraftLocationKey];
            var workdir = new DirectoryInfo(serverRoot);

            if (!workdir.Exists)
                throw new DirectoryNotFoundException($"Minecraft Server directory was not found at {serverRoot}.");

            return workdir;
        }

        private DirectoryInfo guardTempDir()
        {
            var tempLocation = _config[TempLocationKey];
            var tempDir = new DirectoryInfo(tempLocation);

            if (!tempDir.Exists)
                tempDir.Create();

            return tempDir;
        }

        private DirectoryInfo setLogDir()
        {
            var path = Path.Combine(ServerLocation.FullName, "logs");
            return new DirectoryInfo(path);
        }
    }
}
