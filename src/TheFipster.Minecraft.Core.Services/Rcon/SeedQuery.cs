using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Core.Services.Rcon
{
    public class SeedQuery
    {
        private const string SeedCommand = "seed";

        private readonly IRconService _rconService;

        public SeedQuery(IRconService rconService)
            => _rconService = rconService;

        public async Task<long> ExecuteAsync()
        {
            var response = await _rconService.SendAsync(SeedCommand);
            response = response.Replace("[", string.Empty).Replace("]", string.Empty);
            return long.Parse(response);
        }
    }
}
