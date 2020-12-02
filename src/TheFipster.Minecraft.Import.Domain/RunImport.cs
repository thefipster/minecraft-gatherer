using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Domain;

namespace TheFipster.Minecraft.Import.Domain
{
    public class RunImport
    {
        public RunImport()
        {
            Dimensions = new List<DimensionInfo>();
            Logs = new List<LogLine>();
            Stats = new Dictionary<string, Stats>();
            EndScreens = new List<string>();
            Achievements = new Dictionary<string, ICollection<Achievement>>();
            Problems = new List<Problem>();
            Events = new List<RunEvent>();
            PlayerIds = new List<string>();
        }
        public RunImport(WorldInfo worldInfo) : this()
        {
            World = worldInfo;
            Worldname = worldInfo.Name;
        }

        [BsonId]
        public string Worldname { get; set; }

        public WorldInfo World { get; set; }
        public ICollection<DimensionInfo> Dimensions { get; set; }
        public IEnumerable<LogLine> Logs { get; set; }
        public Dictionary<string, Stats> Stats { get; set; }
        public ICollection<string> EndScreens { get; set; }
        public Dictionary<string, ICollection<Achievement>> Achievements { get; set; }
        public ICollection<Problem> Problems { get; set; }
        public ICollection<RunEvent> Events { get; set; }
        public ICollection<string> PlayerIds { get; set; }
    }
}
