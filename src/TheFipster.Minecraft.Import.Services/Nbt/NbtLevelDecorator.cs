using System.Diagnostics;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class NbtLevelDecorator : INbtLoader
    {
        private const string LevelNbtFilename = "level.dat";

        private readonly INbtLoader _component;
        private readonly IConfigService _config;

        public NbtLevelDecorator(INbtLoader component, IConfigService config)
        {
            _component = component;
            _config = config;
        }

        public NbtData Load(WorldInfo world)
        {
            var data = _component.Load(world);

            var levelPath = Path.Combine(_config.ServerLocation.FullName, world.Name, LevelNbtFilename);

            var converter = new Process();
            converter.StartInfo.RedirectStandardOutput = true;
            converter.StartInfo.FileName = _config.PythonLocation.FullName;
            converter.StartInfo.Arguments = $"{_config.NbtConverterLocation} {levelPath}";
            converter.Start();

            var nbt = converter.StandardOutput.ReadToEnd();
            converter.WaitForExit();

            data.Level = nbt;
            return data;
        }
    }
}
