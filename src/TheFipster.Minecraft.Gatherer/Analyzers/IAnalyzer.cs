using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Analyzers
{
    public interface IAnalyzer
    {
        object Analyze(LogSession session);
    }
}
