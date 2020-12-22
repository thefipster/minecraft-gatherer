using System.IO;
using TheFipster.Minecraft.Core.Services;

namespace TheFipster.Minecraft.Overview.Services
{
    public class OverviewerRenderer
    {
        private string OutputFolder = "overviewer";

        private readonly string _overviewerFile;
        private readonly string _renderOutput;

        public OverviewerRenderer(ConfigService config)
        {
            _overviewerFile = config.OverviewerLocation.FullName;
            _renderOutput = Path.Combine(config.DataLocation.FullName, OutputFolder);
        }

        public void Render(string wordname)
        {

        }
    }
}
