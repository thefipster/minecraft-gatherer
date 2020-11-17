using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogLineAnalyzer : ILogAnalyzer
    {
        private readonly ILogAnalyzer _component;
        private readonly ILineAnalyzer _lineAnalyzer;

        public LogLineAnalyzer(ILogAnalyzer component, ILineAnalyzer lineAnalyzer)
        {
            _component = component;
            _lineAnalyzer = lineAnalyzer;
        }
        public ServerLog Analyze(ServerLog log)
        {
            log = _component.Analyze(log);

            foreach (var line in log.Lines)
            {
                var events = _lineAnalyzer.Analyze(line);
                log.Events.AddRange(events);
            }

            return log;
        }
    }
}
