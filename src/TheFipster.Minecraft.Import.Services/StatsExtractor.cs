using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class StatsExtractor : IStatsExtractor
    {
        private readonly IStatsFinder _statsFinder;

        public StatsExtractor(IStatsFinder statsFinder)
            => _statsFinder = statsFinder;

        public Dictionary<string, Stats> Extract(WorldInfo world)
        {
            var allStats = new Dictionary<string, Stats>();
            var files = _statsFinder.Find(world);

            foreach (var file in files)
            {
                var json = File.ReadAllText(file.FullName);
                var playerId = file.Name.Replace(".json", string.Empty);
                var stats = new Stats();
                var rawStat = JsonConvert.DeserializeObject<RawStats>(json);

                stats.Broken = rawStat.Categories.MinecraftBroken;
                stats.Crafted = rawStat.Categories.MinecraftCrafted;
                stats.Custom = rawStat.Categories.MinecraftCustom;
                stats.Dropped = rawStat.Categories.MinecraftDropped;
                stats.Killed = rawStat.Categories.MinecraftKilled;
                stats.KilledBy = rawStat.Categories.MinecraftKilledBy;
                stats.Mined = rawStat.Categories.MinecraftMined;
                stats.PickedUp = rawStat.Categories.MinecraftPickedUp;
                stats.Used = rawStat.Categories.MinecraftUsed;

                allStats.Add(playerId, stats);
            }

            return allStats;
        }
    }
}
