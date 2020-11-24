using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Domain;

namespace TheFipster.Minecraft.Import.Domain
{
    public class RunImport
    {
        public RunImport()
        {
            Dimensions = new List<DimensionInfo>();
            Logs = new List<LogLine>();
            Stats = new Dictionary<string, ICollection<PlayerStats>>();
            EndScreens = new Dictionary<string, bool>();
            Achievements = new Dictionary<string, IEnumerable<Achievement>>();
            Problems = new List<Problem>();
        }

        [BsonId]
        public string Worldname { get; set; }

        public WorldInfo World { get; set; }
        public ICollection<DimensionInfo> Dimensions { get; set; }
        public IEnumerable<LogLine> Logs { get; set; }
        public Dictionary<string, ICollection<PlayerStats>> Stats { get; set; }
        public Dictionary<string, bool> EndScreens { get; set; }
        public Dictionary<string, IEnumerable<Achievement>> Achievements { get; set; }
        public ICollection<Problem> Problems { get; set; }
    }
}
