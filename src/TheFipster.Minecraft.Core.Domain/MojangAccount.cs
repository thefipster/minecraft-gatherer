using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Core.Domain
{
    public class MojangAccount
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        [JsonProperty("selectedProfile")]
        public Profile SelectedProfile { get; set; }

        [JsonProperty("availableProfiles")]
        public ICollection<Profile> AvailableProfiles { get; set; }
    }

    public class Profile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class User
    {
        [JsonProperty("properties")]
        public ICollection<Property> Properties { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
