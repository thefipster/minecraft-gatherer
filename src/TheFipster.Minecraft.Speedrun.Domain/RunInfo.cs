using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class RunInfo
    {
        public RunInfo()
        {
            Players = new List<Player>();
            Logs = new List<LogLine>();
        }

        public WorldInfo World { get; set; }
        public List<Player> Players { get; set; }
        public List<LogLine> Logs { get; set; }
    }
}
