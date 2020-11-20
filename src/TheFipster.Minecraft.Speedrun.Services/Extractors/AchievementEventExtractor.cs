using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class AchievementEventExtractor : IAchievementEventExtractor
    {
        private const string AchievementFolder = "advancements";
        private const string TokenNamespace = "$..minecraft:";

        private Dictionary<string, string> eventTranslation = new Dictionary<string, string>
        {
            { "killed_dragon", EventNames.FreeTheEnd },
            { "has_cobblestone", EventNames.StoneAge },
            { "stone_pickaxe", EventNames.GettingAnUpgrade },
            { "entered_nether", EventNames.WeNeedToGoDeeper },
            { "entered_end", EventNames.TheEnd },
            { "in_stronghold", EventNames.EyeSpy },
            { "fortress", EventNames.ATerribleFortress },
            { "has_blaze_rod", EventNames.IntoFire },
            { "has_blaze_powder",EventNames.GotBlazePowder },
            { "has_bed", EventNames.GotBed },
            { "has_log", EventNames.PunchedATree },
            { "has_flint", EventNames.GotFlint },
            { "has_iron_ore", EventNames.GotIronOre },
            { "has_gold_ingot", EventNames.GotGoldIngot },
            { "has_gravel", EventNames.GotGravel },
            { "has_string", EventNames.GotString },
            { "has_gold_nugget", EventNames.GotGoldNugget }
        };

        private Dictionary<string, string> recipeTranslation = new Dictionary<string, string>
        {
            { "adventure/kill_a_mob", EventNames.MonsterHunter }
        };

        private readonly IConfigService _config;
        private readonly IPlayerStore _playerStore;

        public AchievementEventExtractor(IConfigService config, IPlayerStore playerStore)
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
            var tokens = jObject.SelectTokens($"{TokenNamespace}{translation.Key}");
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
