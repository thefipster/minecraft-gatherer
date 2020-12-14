using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Core.Services;
using Xunit;

namespace TheFipster.Minecraft.Core.IntegrationTest
{
    public class RconServiceTest
    {
        [Fact]
        public void GetSeedTest()
        {

            var logger = Substitute.For<ILogger<RconService>>();

            var settings = new RconConfig("minecraft.thefipster.com", 25576, "coolerpups7BCG8uv");

            var section = Substitute.For<IConfigurationSection>();
            section.Key.Returns(RconService.RconConfigSection);
            section.Value.Returns(JsonConvert.SerializeObject(settings));

            var config = Substitute.For<IConfiguration>();
            config.GetSection(RconService.RconConfigSection).Returns(section);

            var service = new RconService(config, logger);

            var seed = service.SendAsync("seed");
        }
    }
}
