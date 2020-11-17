using System;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogAnalyzer : ILogAnalyzer
    {
        public ServerLog Analyze(ServerLog log)
        {
            log.AnalyzedOn = DateTime.UtcNow;
            return log;
        }
    }
}
