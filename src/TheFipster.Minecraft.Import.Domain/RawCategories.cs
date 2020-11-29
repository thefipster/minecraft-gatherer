using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Domain
{
    public class RawCategories
    {
        [JsonProperty("minecraft:killed", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftKilled { get; set; }

        [JsonProperty("minecraft:killed_by", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftKilledBy { get; set; }

        [JsonProperty("minecraft:used", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftUsed { get; set; }

        [JsonProperty("minecraft:crafted", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftCrafted { get; set; }

        [JsonProperty("minecraft:custom", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftCustom { get; set; }

        [JsonProperty("minecraft:dropped", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftDropped { get; set; }

        [JsonProperty("minecraft:mined", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftMined { get; set; }

        [JsonProperty("minecraft:picked_up", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftPickedUp { get; set; }

        [JsonProperty("minecraft:broken", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinecraftBroken { get; set; }
    }
}
