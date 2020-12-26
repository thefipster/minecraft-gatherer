using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class OverviewerRenderer : IMapRenderer
    {
        private string OverviewerFolder = "overviewer";

        private readonly string _pythonExecutable;
        private readonly string _overviewerFile;
        private readonly string _renderPath;
        private readonly string _tempPath;

        private readonly IWorldFinder _finder;

        public OverviewerRenderer(IConfigService config, IWorldFinder worldFinder)
        {
            _pythonExecutable = config.PythonLocation.FullName;
            _overviewerFile = config.OverviewerLocation.FullName;
            _renderPath = Path.Combine(config.DataLocation.FullName, OverviewerFolder);
            _tempPath = Path.Combine(config.TempLocation.FullName, OverviewerFolder);

            _finder = worldFinder;
        }

        public RenderResult Render(string worldname)
        {
            var result = new RenderResult(worldname);

            try
            {
                result.StartedOn = DateTime.UtcNow;
                var configFilepath = createRenderConfig(worldname);
                var stdout = runOverviewer(configFilepath);
                result.EndedOn = DateTime.UtcNow;

                result.Success = true;
                result.StdOut = stdout;
            }
            catch (Exception ex)
            {
                result.EndedOn = DateTime.UtcNow;
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        private string createRenderConfig(string worldname)
        {
            var template = readTemplate();

            var location = _finder.Locate(worldname).OfType<DirectoryInfo>().FirstOrDefault();
            if (location == null)
                throw new Exception("World couldn't be located for map rendering.");

            var input = location.FullName;
            var output = Path.Combine(_renderPath, worldname);

            var config = string.Format(template, input, output);
            var configPath = Path.Combine(_tempPath, $"{worldname}.ovc");

            File.WriteAllText(configPath, config);
            return configPath;
        }

        private string runOverviewer(string configFilepath)
        {
            var converter = new Process();
            converter.StartInfo.RedirectStandardOutput = true;
            converter.StartInfo.FileName = _pythonExecutable;
            converter.StartInfo.Arguments = $"{_overviewerFile} --config={configFilepath}";
            converter.Start();

            var stdout = converter.StandardOutput.ReadToEnd();
            converter.WaitForExit();
            return stdout;
        }

        private string readTemplate()
        {
            var assembly = Assembly.GetAssembly(GetType());
            var resourceName = "config-template.txt";

            string template = string.Empty;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }

            return template;
        }
    }
}
