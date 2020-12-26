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

            ServerLocation = checkExistance(_paths.Server) as DirectoryInfo;
            LogLocation = checkExistance(_paths.Logs) as DirectoryInfo;
            PythonLocation = checkExistance(_paths.Python) as FileInfo;
            NbtConverterLocation = checkExistance(_paths.NbtConverter) as FileInfo;
            OverviewerLocation = checkExistance(_paths.Overviewer) as FileInfo;

            TempLocation = ensureExistance(_paths.Temp);
            DataLocation = ensureExistance(_paths.Data);

            Uri.TryCreate(_paths.OverviewerUrl, UriKind.Absolute, out var url);
            OverviewerUrl = url;

            InitialRunIndex = ensureInt(InitialRunIndexKey);
        }

        public DirectoryInfo ServerLocation { get; }
        public DirectoryInfo LogLocation { get; }
        public DirectoryInfo TempLocation { get; }
        public DirectoryInfo DataLocation { get; }
        public FileInfo PythonLocation { get; }
        public FileInfo NbtConverterLocation { get; }
        public FileInfo OverviewerLocation { get; }
        public Uri OverviewerUrl { get; }
        public int InitialRunIndex { get; }

        private FileSystemInfo checkExistance(string path)
        {
            if (Directory.Exists(path))
                return new DirectoryInfo(path);

            if (File.Exists(path))
                return new FileInfo(path);

            throw new DirectoryNotFoundException($"{path} doesn't exist.");
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
