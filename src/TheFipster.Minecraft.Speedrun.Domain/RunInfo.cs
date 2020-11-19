using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class RunInfo
    {
        public RunInfo()
        {
            Players = new List<Player>();
            Splits = new List<Split>();
            Stats = new List<PlayerStats>();
        }

        public string Id { get; set; }
        public int Index { get; set; }
        public WorldInfo World { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public ServerLog Logs { get; set; }
        public IEnumerable<Split> Splits { get; set; }
        public IEnumerable<PlayerStats> Stats { get; set; }
        public ValidityResult Validity { get; set; }
        public OutcomeResult Outcome { get; set; }
        public IEnumerable<GameEvent> Achievements { get; set; }
    }
}
