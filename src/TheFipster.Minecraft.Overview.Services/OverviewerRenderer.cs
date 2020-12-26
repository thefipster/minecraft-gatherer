using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class OverviewerRenderer : IMapRenderer
    {
        private string OverviewerFolder = "overviewer";

        private readonly string _pythonExecutable;
        private readonly string _overviewerFile;
        private readonly string _tempPath;


        public OverviewerRenderer(IConfigService config)
        {
            _pythonExecutable = config.PythonLocation.FullName;
            _overviewerFile = config.OverviewerLocation.FullName;
            _tempPath = Path.Combine(config.TempLocation.FullName, OverviewerFolder);
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
