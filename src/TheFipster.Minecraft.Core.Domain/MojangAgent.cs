using Newtonsoft.Json;

namespace TheFipster.Minecraft.Core.Domain
{
    public class MojangAgent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }

        public static MojangAgent CreateDefault()
        {
            return new MojangAgent
            {
                Name = "Minecraft",
                Version = 1
            };
        }
    }
}
