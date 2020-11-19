using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Domain.Stats;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class StatsExtractor : IStatsExtractor
    {
        private readonly IStatsFinder _statsFinder;

        public StatsExtractor(IStatsFinder statsFinder)
        {
            _statsFinder = statsFinder;
        }

        public IEnumerable<PlayerStats> Extract(string worldName)
        {
            var stats = new List<PlayerStats>();
            var files = _statsFinder.Find(worldName);

            var rawStats = new List<RawStats>();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file.FullName);
                var playerId = file.Name.Replace(".json", string.Empty);
                var cleanedStats = new PlayerStats(playerId);
                var rawStat = JsonConvert.DeserializeObject<RawStats>(json);

                cleanedStats.Broken = clean(rawStat.Categories.MinecraftBroken);
                cleanedStats.Crafted = clean(rawStat.Categories.MinecraftCrafted);
                cleanedStats.Custom = clean(rawStat.Categories.MinecraftCustom);
                cleanedStats.Dropped = clean(rawStat.Categories.MinecraftDropped);
                cleanedStats.Killed = clean(rawStat.Categories.MinecraftKilled);
                cleanedStats.KilledBy = clean(rawStat.Categories.MinecraftKilledBy);
                cleanedStats.Mined = clean(rawStat.Categories.MinecraftMined);
                cleanedStats.PickedUp = clean(rawStat.Categories.MinecraftPickedUp);
                cleanedStats.Used = clean(rawStat.Categories.MinecraftUsed);

                stats.Add(cleanedStats);
            }

            return stats;
        }

        private Dictionary<string, double> clean(Dictionary<string, long> raw)
        {
            var clean = new Dictionary<string, double>();

            if (raw == null)
                return clean;

            foreach (var entry in raw)
            {
                var newKey = entry.Key.Replace("minecraft:", string.Empty).Replace("-", " ").Replace("_", " ");
                var newFirstChar = newKey.Substring(0, 1).ToUpper();
                newKey = newFirstChar + newKey.Substring(1);
                clean.Add(newKey, entry.Value);
            }

            return clean;
        }
    }
}
