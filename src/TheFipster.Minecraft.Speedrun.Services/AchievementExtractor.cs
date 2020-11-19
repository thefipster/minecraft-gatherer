using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class AchievementExtractor : IAchievementExtractor
    {
        private const string AchievementFolder = "advancements";

        private Dictionary<string, string> eventTranslation = new Dictionary<string, string>
        {
            { "killed_dragon", "Free the End" },
            { "has_cobblestone", "Stone Age" },
            { "stone_pickaxe", "Getting an Upgrade" },
            { "entered_nether", "We Need to Go Deeper" },
            { "entered_end", "The End?" },
            { "in_stronghold", "Eye Spy" },
            { "fortress", "A Terrible Fortress" },
            { "has_blaze_rod", "Into Fire" },
            { "has_blaze_powder", "Blaze Powder" },
            { "has_bed", "Let's sleep" },
            { "has_log", "Punched a tree" },
            { "has_flint", "Got Flient" },
            { "has_iron_ore", "Got Iron Ore" },
            { "has_gold_ingot", "Got Gold Ingot" },
            { "has_gravel", "Got Gravel" },
            { "has_string", "Got String" },
            { "has_gold_nugget", "Got Gold Nugget" }
        };

        private Dictionary<string, string> recipeTranslation = new Dictionary<string, string>
        {
            { "adventure/kill_a_mob", "Monster Hunter" }
        };



        private IConfigService _config;
        private readonly IPlayerStore _playerStore;

        public AchievementExtractor(IConfigService config, IPlayerStore playerStore)
        {
            _config = config;
            _playerStore = playerStore;
        }

        public IEnumerable<GameEvent> Extract(WorldInfo world)
        {
            var path = Path.Combine(_config.ServerLocation.FullName, world.Name, AchievementFolder);
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                return Enumerable.Empty<GameEvent>();

            var files = dir.GetFiles("*.json");
            var events = new List<GameEvent>();
            foreach (var file in files)
            {
                var playerId = file.Name.Replace(".json", string.Empty);
                var player = _playerStore.GetPlayerById(playerId);

                var text = File.ReadAllText(file.FullName);
                var json = (JObject)JsonConvert.DeserializeObject(text);

                foreach (var translation in eventTranslation)
                {
                    var gameEvent = extractFromEvent(player, json, translation);
                    events.Add(gameEvent);
                }

                foreach (var translation in recipeTranslation)
                {
                    var gameEvent = extractFromRecipe(player, json, translation);
                    events.Add(gameEvent);
                }
            }

            return events.Where(x => x != null);
        }

        private GameEvent extractFromEvent(Player player, JObject jObject, KeyValuePair<string, string> translation)
        {
            var tokens = jObject.SelectTokens($"$..{translation.Key}");
            if (tokens.Count() == 0)
                return null;

            var timestamp = extractTimestamp(tokens);
            return GameEvent.CreateAchievement(player, timestamp, translation.Value);
        }

        private GameEvent extractFromRecipe(Player player, JObject jObject, KeyValuePair<string, string> translation)
        {
            var tokens = jObject.SelectTokens($"$..minecraft:{translation.Key}");
            if (tokens.Count() == 0)
                return null;

            var value = tokens.First().First().First().First();
            var timestamp = extractTimestamp(value);
            return GameEvent.CreateAchievement(player, timestamp, translation.Value);

        }

        private DateTime extractTimestamp(IEnumerable<JToken> tokens)
        {
            var token = tokens.First();
            var value = token.Value<string>();
            var timestamp = DateTime.Parse(value);
            return timestamp;
        }

    }
}
