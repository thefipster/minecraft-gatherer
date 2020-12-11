using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services
{
    public class NbtPlayerConverter : INbtPlayerConverter
    {
        public ICollection<NbtPlayer> Convert(RunImport run)
        {
            var players = new List<NbtPlayer>();

            if (run.NbtData == null || run.NbtData.Players == null || run.NbtData.Players.Count() == 0)
                return players;

            foreach (var nbt in run.NbtData.Players)
            {
                var playerId = nbt.Key;
                var nbtPlayer = new NbtPlayer(playerId);

                var json = nbt.Value;
                var jObj = JsonConvert.DeserializeObject(json) as JObject;

                var hurtTimestamp = jObj.SelectToken("$..HurtByTimestamp").Value<long>();
                nbtPlayer.HurtTimestamp = hurtTimestamp;

                var sleepTimer = jObj.SelectToken("$..SleepTimer").Value<long>();
                nbtPlayer.SleepTimer = sleepTimer;

                var absorptionAmount = jObj.SelectToken("$..AbsorptionAmount").Value<int>();
                nbtPlayer.AbsorptionAmount = absorptionAmount;

                var deathTime = jObj.SelectToken("$..DeathTime").Value<long>();
                nbtPlayer.DeathTime = deathTime;

                var xpSeed = jObj.SelectToken("$..XpSeed").Value<int>();
                nbtPlayer.XpSeed = xpSeed;

                var xpTotal = jObj.SelectToken("$..XpTotal").Value<int>();
                nbtPlayer.XpTotal = xpTotal;

                var seenCredits = jObj.SelectToken("$..seenCredits").Value<int>();
                nbtPlayer.SeenCredits = System.Convert.ToBoolean(seenCredits);

                var health = jObj.SelectToken("$..Health").Value<int>();
                nbtPlayer.Health = health;

                var foodSaturationLevel = jObj.SelectToken("$..foodSaturationLevel").Value<int>();
                nbtPlayer.FoodSaturationLevel = foodSaturationLevel;

                var xpLevel = jObj.SelectToken("$..XpLevel").Value<int>();
                nbtPlayer.XpLevel = xpLevel;

                var score = jObj.SelectToken("$..Score").Value<long>();
                nbtPlayer.Score = score;

                var spawnXToken = jObj.SelectToken("$..SpawnX");
                var spawnYToken = jObj.SelectToken("$..SpawnY");
                var spawnZToken = jObj.SelectToken("$..SpawnZ");

                if (spawnXToken != null && spawnYToken != null && spawnZToken != null)
                {
                    var spawnX = spawnXToken.Value<int>();
                    var spawnY = spawnYToken.Value<int>();
                    var spawnZ = spawnZToken.Value<int>();
                    nbtPlayer.Spawn = new Coordinate(spawnX, spawnY, spawnZ);
                }

                players.Add(nbtPlayer);
            }

            return players;
        }
    }
}
