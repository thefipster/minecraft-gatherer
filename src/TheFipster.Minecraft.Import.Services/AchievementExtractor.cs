using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class AchievementExtractor : IAchievementExtractor
    {
        private const string AchievementFolder = "advancements";

        private readonly IConfigService _config;

        public AchievementExtractor(IConfigService config)
        {
            _config = config;
        }

        public Dictionary<string, ICollection<Achievement>> Extract(WorldInfo world)
        {
            var playerAchievements = new Dictionary<string, ICollection<Achievement>>();

            var path = Path.Combine(_config.ServerLocation.FullName, world.Name, AchievementFolder);
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                return playerAchievements;

            var files = dir.GetFiles("*.json");

            foreach (var file in files)
            {
                var achievements = new List<Achievement>();
                var playerId = file.Name.Replace(".json", string.Empty);

                var text = File.ReadAllText(file.FullName);
                var json = (JObject)JsonConvert.DeserializeObject(text);

                var criterias = json.SelectTokens("$..criteria");
                foreach (var criteria in criterias)
                {
                    foreach (var child in criteria.Children())
                    {
                        var property = child as JProperty;
                        var name = property.Name;
                        var timestamp = extractTimestamp(property);

                        var achievement = new Achievement(name, timestamp);
                        achievements.Add(achievement);
                    }
                }

                var distinctCriteria = achievements.Select(x => x.Event).Distinct();
                var distinctAchievements = new List<Achievement>();
                foreach (var critirea in distinctCriteria)
                {
                    var distinctAchievement = achievements.First(x => x.Event == critirea);
                    distinctAchievements.Add(distinctAchievement);
                }

                playerAchievements.Add(playerId, distinctAchievements);
            }

            return playerAchievements;
        }

        private DateTime extractTimestamp(JProperty property)
        {
            var token = property.Value;
            var value = token.Value<string>();
            var timestamp = DateTime.Parse(value);
            return timestamp;
        }
    }
}
