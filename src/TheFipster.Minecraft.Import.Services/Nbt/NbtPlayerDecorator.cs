using System.Diagnostics;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class NbtPlayerDecorator : INbtLoader
    {
        private const string PlayerFolder = "playerdata";

        private readonly INbtLoader _component;
        private readonly IConfigService _config;

        public NbtPlayerDecorator(INbtLoader component, IConfigService config)
        {
            _component = component;
            _config = config;
        }

        public NbtData Load(WorldInfo world)
        {
            var data = _component.Load(world);

            var playerPath = Path.Combine(_config.ServerLocation.FullName, world.Name, PlayerFolder);
            if (!Directory.Exists(playerPath))
                return data;

            var playerFiles = Directory.GetFiles(playerPath, "*.dat");

            foreach (var filepath in playerFiles)
            {
                var file = new FileInfo(filepath);
                var playerId = file.Name.Replace(".dat", string.Empty);
                var nbt = loadNbt(filepath);
                data.Players.Add(playerId, nbt);

            }

            return data;
        }

        private string loadNbt(string path)
        {
            var converter = new Process();
            converter.StartInfo.RedirectStandardOutput = true;
            converter.StartInfo.FileName = _config.PythonLocation.FullName;
            converter.StartInfo.Arguments = $"{_config.NbtConverterLocation} {path}";
            converter.Start();

            var output = converter.StandardOutput.ReadToEnd();
            converter.WaitForExit();
            return output;
        }
    }
}
