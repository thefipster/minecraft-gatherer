using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class LineAnalyzerDecorator : ILineAnalyzer
    {
        private readonly ILineAnalyzer _component;
        private readonly IPlayerStore _playerStore;

        public LineAnalyzerDecorator(ILineAnalyzer component, IPlayerStore playerStore)
        {
            _component = component;
            _playerStore = playerStore;
        }

        public List<LogEvent> Analyze(LogLine line)
        {
            var events = _component.Analyze(line);

            if (!line.Message.Contains("Set the time"))
                return events;

            //var @event = LogEvent.Create;

            return events;
        }
    }
}
