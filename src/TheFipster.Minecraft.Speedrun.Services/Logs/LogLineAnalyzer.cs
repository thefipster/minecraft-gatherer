using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LogLineAnalyzer : ILogEventExtractor
    {
        private readonly ILogEventExtractor _component;
        private readonly ILineAnalyzer _lineAnalyzer;

        public LogLineAnalyzer(ILogEventExtractor component, ILineAnalyzer lineAnalyzer)
        {
            _component = component;
            _lineAnalyzer = lineAnalyzer;
        }
        public IEnumerable<GameEvent> Extract(IEnumerable<LogLine> log)
        {
            var events = _component.Extract(log).ToList();

            foreach (var line in log)
            {
                var newEvents = _lineAnalyzer.Analyze(line);
                events.AddRange(newEvents);
            }

            return events;
        }
    }
}
