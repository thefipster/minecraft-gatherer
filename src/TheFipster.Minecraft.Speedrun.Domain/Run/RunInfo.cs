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
            EndScreens = new Dictionary<string, bool>();
            Problems = new List<Problem>();
        }

        public RunInfo(WorldInfo world) : this()
        {
            World = world;
            Id = World.Name;
        }

        public string Id { get; set; }
        public int Index { get; set; }
        public WorldInfo World { get; set; }
        public ValidityResult Validity { get; set; }
        public OutcomeResult Outcome { get; set; }
        public Timings Timings { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<PlayerStats> Stats { get; set; }
        public List<GameEvent> Events { get; set; }
        public IEnumerable<LogLine> Logs { get; set; }
        public Dictionary<string, bool> EndScreens { get; set; }
        public ICollection<Problem> Problems { get; set; }
    }
}
