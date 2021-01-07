using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Meta.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("meta")]
    public class MetaController : Controller
    {
        private readonly ITimingBlacklister _timingBlacklister;

        public MetaController(ITimingBlacklister timingBlacklister)
        {
            _timingBlacklister = timingBlacklister;
        }

        [HttpGet("timing/blacklist/add/{worldname}")]
        public IActionResult AddTimingBlacklist(string worldname)
        {
            _timingBlacklister.Add(worldname);
            return RedirectToAction("Name", "Run", new { worldname });
        }

        [HttpGet("timing/blacklist/remove/{worldname}")]
        public IActionResult RemoveTimingBlacklist(string worldname)
        {
            _timingBlacklister.Remove(worldname);
            return RedirectToAction("Name", "Run", new { worldname });
        }

        [HttpGet("timing/blacklist/{worldname}")]
        public JsonResult IsTimingBlacklist(string worldname)
        {
            var isBlacklisted = _timingBlacklister.IsBlacklisted(worldname);
            return Json(isBlacklisted);
        }
    }
}