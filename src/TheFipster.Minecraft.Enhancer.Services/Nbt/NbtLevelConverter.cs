using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services
{
    public class NbtLevelConverter : INbtLevelConverter
    {
        public NbtLevel Convert(RunImport run)
        {
            if (run.NbtData == null || string.IsNullOrWhiteSpace(run.NbtData.Level))
                return null;

            var jObj = JsonConvert.DeserializeObject(run.NbtData.Level) as JObject;
            var nbtLevel = new NbtLevel();

            var difficulty = jObj.SelectToken("$..Difficulty").Value<int>();
            nbtLevel.Difficulty = (Difficulty)difficulty;

            var time = jObj.SelectToken("$..Time").Value<long>();
            nbtLevel.Time = time;

            var gameType = jObj.SelectToken("$..GameType").Value<int>();
            nbtLevel.GameMode = (GameMode)gameType;

            var dayTime = jObj.SelectToken("$..DayTime").Value<long>();
            nbtLevel.Daytime = dayTime;

            var dragonKilled = jObj.SelectToken("$..DragonFight").SelectToken("DragonKilled").Value<int>();
            nbtLevel.DragonKilled = System.Convert.ToBoolean(dragonKilled);

            var levelName = jObj.SelectToken("$..LevelName").Value<string>();
            nbtLevel.Worldname = levelName;

            var lastPlayed = jObj.SelectToken("$..LastPlayed").Value<long>();
            var lastPlayedOn = DateTimeOffset.FromUnixTimeMilliseconds(lastPlayed).DateTime;
            nbtLevel.LastPlayedOn = lastPlayedOn;

            var hardcore = jObj.SelectToken("$..hardcore").Value<int>();
            nbtLevel.IsHardcore = System.Convert.ToBoolean(hardcore);

            var spawnX = jObj.SelectToken("$..SpawnX").Value<int>();
            var spawnY = jObj.SelectToken("$..SpawnY").Value<int>();
            var spawnZ = jObj.SelectToken("$..SpawnZ").Value<int>();
            nbtLevel.Spawn = new Coordinate(spawnX, spawnY, spawnZ);

            var worldGen = jObj.SelectToken("$..WorldGenSettings");
            var levelSeed = worldGen.SelectToken("seed").Value<long>();
            nbtLevel.Seed = levelSeed;

            var generators = worldGen.SelectTokens("$..generator");

            var version = jObj.SelectToken("$..Version");
            var versionName = version.SelectToken("Name").Value<string>();
            var versionId = version.SelectToken("Id").Value<string>();
            var snapshot = version.SelectToken("Snapshot").Value<string>();
            nbtLevel.Version = new MinecraftVersion(versionId, versionName, snapshot);

            foreach (var generator in generators)
            {
                var dimensionName = generator.SelectToken("settings").Value<string>();
                var dimensionSeed = generator.SelectToken("seed").Value<long>();

                var dimension = DimensionTranslations.Items.First(x => x.Value.Id == dimensionName);
                nbtLevel.DimensionSeeds.Add(dimension.Key, dimensionSeed);
            }

            return nbtLevel;
        }
    }
}
