using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class RunInfo
    {
        public RunInfo()
        {
            Players = new List<Player>();
        }

        public string Id { get; set; }
        public int Index { get; set; }
        public WorldInfo World { get; set; }
        public List<Player> Players { get; set; }
        public ServerLog Logs { get; set; }
        public List<Split> Splits { get; set; }
        public IEnumerable<PlayerStats> Stats { get; set; }
        public ValidityResult Validity { get; set; }
        public OutcomeResult Outcome { get; set; }
    }
}
