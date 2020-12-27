using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Authorize]
    public class ServerController : Controller
    {
        private readonly IServerPropertiesReader _serverPropertiesReader;
        private readonly IWhitelistReader _whitelistReader;
        private readonly IOpsReader _opsReader;

        public ServerController(
            IServerPropertiesReader serverPropertiesReader,
            IWhitelistReader whitelistReader,
            IOpsReader opsReader)
        {
            _serverPropertiesReader = serverPropertiesReader;
            _whitelistReader = whitelistReader;
            _opsReader = opsReader;
        }

        public IActionResult Index()
        {
            var viewmodel = new ServerIndexViewModel();
            viewmodel.ServerProperties = _serverPropertiesReader.Read();
            viewmodel.WhitelistedPlayers = _whitelistReader.Read();
            viewmodel.Operators = _opsReader.Read();
            return View(viewmodel);
        }
    }
}