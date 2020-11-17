using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain.Stats
{
    public class RawCategories
    {
        [JsonProperty("minecraft:killed", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftKilled { get; set; }

        [JsonProperty("minecraft:killed_by", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftKilledBy { get; set; }

        [JsonProperty("minecraft:used", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftUsed { get; set; }

        [JsonProperty("minecraft:crafted", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftCrafted { get; set; }

        [JsonProperty("minecraft:custom", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftCustom { get; set; }

        [JsonProperty("minecraft:dropped", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftDropped { get; set; }

        [JsonProperty("minecraft:mined", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftMined { get; set; }

        [JsonProperty("minecraft:picked_up", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftPickedUp { get; set; }

        [JsonProperty("minecraft:broken", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, long> MinecraftBroken { get; set; }
    }
}
