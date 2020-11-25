using Newtonsoft.Json;

namespace TheFipster.Minecraft.Import.Domain
{
    public class RawStats
    {
        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public RawCategories Categories { get; set; }

        [JsonProperty("DataVersion", NullValueHandling = NullValueHandling.Ignore)]
        public long? DataVersion { get; set; }
    }
}
