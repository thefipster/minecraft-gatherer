using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class ServerController : Controller
    {
        private readonly IServerPropertiesReader _serverPropertiesReader;

        public ServerController(IServerPropertiesReader serverPropertiesReader)
            => _serverPropertiesReader = serverPropertiesReader;

        public IActionResult Index()
        {
            var viewmodel = new ServerIndexViewModel();
            viewmodel.ServerProperties = _serverPropertiesReader.Read();
            return View(viewmodel);
        }
    }
}