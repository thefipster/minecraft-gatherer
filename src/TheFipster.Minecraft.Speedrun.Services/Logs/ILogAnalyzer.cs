using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILogAnalyzer
    {
        ServerLog Analyze(ServerLog log);
    }
}
