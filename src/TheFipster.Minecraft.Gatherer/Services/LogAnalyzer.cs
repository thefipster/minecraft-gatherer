using System.Collections.Generic;
using TheFipster.Minecraft.Gatherer.Analyzers;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LogAnalyzer
    {
        private readonly IEnumerable<IAnalyzer> analyzers;

        public LogAnalyzer(List<IAnalyzer> analyzers)
        {
            this.analyzers = analyzers;
        }

        public LogAnalyzer(params IAnalyzer[] analyzers)
        {
            this.analyzers = analyzers;
        }

        public void Analyze(LogSession session)
        {
            foreach (var analyzer in analyzers)
            {
                var result = analyzer.Analyze(session);
            }
        }
    }
}
