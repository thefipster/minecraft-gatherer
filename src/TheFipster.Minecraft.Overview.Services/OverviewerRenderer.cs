using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class OverviewerRenderer : IMapRenderer
    {
        private const string OverviewerFolder = "overviewer";
        private const string ConfigTemplateName = "config-template.txt";

        private readonly string _pythonExecutable;
        private readonly string _overviewerFile;
        private readonly string _tempPath;
        private readonly IHostEnv _hostEnv;

        public OverviewerRenderer(IConfigService config, IHostEnv hostEnv)
        {
            _pythonExecutable = config.PythonLocation.FullName;
            _overviewerFile = config.OverviewerLocation.FullName;
            _tempPath = Path.Combine(config.TempLocation.FullName, OverviewerFolder);
            _hostEnv = hostEnv;
        }

        public RenderResult Render(string worldname, string worldFolder, string outputFolder)
        {
            var result = new RenderResult(worldname);

            try
            {
                result.StartedOn = DateTime.UtcNow;
                var configFilepath = createRenderConfig(worldname, worldFolder, outputFolder);
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

        private string createRenderConfig(string worldname, string input, string output)
        {
            var template = readTemplate();

            input = input.Replace("\\", "/");
            output = output.Replace("\\", "/");
            var config = template.Replace("{{input}}", input).Replace("{{output}}", output);
            var configPath = Path.Combine(_tempPath, $"{worldname}.ovc");

            File.WriteAllText(configPath, config);
            return configPath;
        }

        private string runOverviewer(string configFilepath)
        {
            var converter = new Process();
            converter.StartInfo.RedirectStandardOutput = true;

            if (_hostEnv.IsLinux)
            {
                converter.StartInfo.FileName = _pythonExecutable;
                converter.StartInfo.Arguments = $"{_overviewerFile} --config={configFilepath}";
            }
            else
            {
                converter.StartInfo.FileName = _overviewerFile;
                converter.StartInfo.Arguments = $"--config={configFilepath}";
            }


            converter.Start();

            var stdout = converter.StandardOutput.ReadToEnd();
            converter.WaitForExit();
            return stdout;
        }

        private string readTemplate()
        {
            var assembly = Assembly.GetAssembly(GetType());

            string template = string.Empty;

            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(ConfigTemplateName));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }

            return template;
        }
    }
}
