using Newtonsoft.Json;

namespace TheFipster.Minecraft.Core.Domain
{
    public class ServerPlayer : IPlayer
    {
        [JsonProperty("uuid")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("bypassesPlayerLimit")]
        public bool CanBypass { get; set; }
    }
}
