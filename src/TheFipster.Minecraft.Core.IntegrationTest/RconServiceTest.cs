using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Core.Services;
using Xunit;

namespace TheFipster.Minecraft.Core.IntegrationTest
{
    public class RconServiceTest
    {
        [Fact]
        public async Task GetSeedTestAsync()
        {
            var rconConfig = new RconConfig("minecraft.thefipster.com", 25576, "coolerpups7BCG8uv");
            var settings = new { Rcon = rconConfig };
            var json = JsonConvert.SerializeObject(settings);

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            var config = builder.Build();

            var logger = Substitute.For<ILogger<RconService>>();

            var service = new RconService(config, logger);

            var seed = await service.SendAsync("seed");

            seed.Should().NotBeNullOrEmpty();
            long.Parse(seed);
        }
    }
}
