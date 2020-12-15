using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class MojangService : IMojangService
    {
        private const string AuthUrl = "https://authserver.mojang.com/authenticate";

        public async Task<MojangAccount> VerifyAccountAsync(string username, string password)
        {
            var response = await makeRequestAsync(username, password);
            return await convertToAccountAsync(response);
        }

        private async Task<HttpResponseMessage> makeRequestAsync(string username, string password)
        {
            using var http = new HttpClient();
            var request = createRequest(username, password);
            var response = await http.PostAsync(AuthUrl, request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<MojangAccount> convertToAccountAsync(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<MojangAccount>(body);
            return account;
        }

        private StringContent createRequest(string username, string password)
        {
            var request = new MojangAuthenticationRequest(username, password);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
