using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Services.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class StatsExtractor : IStatsExtractor
    {
        private readonly IStatsFinder _statsFinder;

        public StatsExtractor(IStatsFinder statsFinder)
        {
            _statsFinder = statsFinder;
        }

        public Dictionary<string, Stats> Extract(WorldInfo world)
        {
            var stats = new Dictionary<string, Stats>();
            var files = _statsFinder.Find(world);

            foreach (var file in files)
            {
                var json = File.ReadAllText(file.FullName);
                var playerId = file.Name.Replace(".json", string.Empty);
                var cleanedStats = new Stats();
                var rawStat = JsonConvert.DeserializeObject<RawStats>(json);

                cleanedStats.Broken = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftBroken);
                cleanedStats.Crafted = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftCrafted);
                cleanedStats.Custom = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftCustom);
                cleanedStats.Dropped = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftDropped);
                cleanedStats.Killed = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftKilled);
                cleanedStats.KilledBy = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftKilledBy);
                cleanedStats.Mined = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftMined);
                cleanedStats.PickedUp = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftPickedUp);
                cleanedStats.Used = convertToHumanFriendlyKeyName(rawStat.Categories.MinecraftUsed);

                stats.Add(playerId, cleanedStats);
            }

            return stats;
        }

        private Dictionary<string, double> convertToHumanFriendlyKeyName(Dictionary<string, long> raw)
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
