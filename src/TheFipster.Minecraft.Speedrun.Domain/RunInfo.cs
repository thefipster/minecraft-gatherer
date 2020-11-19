using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class RunInfo
    {
        public RunInfo()
        {
            Players = new List<Player>();
            Stats = new List<PlayerStats>();
            Events = new List<GameEvent>();
            Logs = new List<LogLine>();
        }

        public string Id { get; set; }
        public int Index { get; set; }
        public WorldInfo World { get; set; }
        public ValidityResult Validity { get; set; }
        public OutcomeResult Outcome { get; set; }
        public Timings Timings { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<PlayerStats> Stats { get; set; }
        public List<GameEvent> Events { get; set; }
        public IEnumerable<LogLine> Logs { get; set; }
    }
}
