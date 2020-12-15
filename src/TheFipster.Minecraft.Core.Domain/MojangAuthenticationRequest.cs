using Newtonsoft.Json;

namespace TheFipster.Minecraft.Core.Domain
{
    public class MojangAuthenticationRequest
    {
        public MojangAuthenticationRequest(string username, string password)
        {
            Agent = MojangAgent.CreateDefault();
            Username = username;
            Password = password;
        }

        [JsonProperty("agent")]
        public MojangAgent Agent { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }


        [JsonProperty("requestUser")]
        public bool RequestUser => true;
    }
}
