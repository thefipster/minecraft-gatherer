using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Extender.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class StatsController : Controller
    {
        private readonly IAttemptHeatmapExtender _attemptHeatmapExtender;

        public StatsController(IAttemptHeatmapExtender attemptHeatmapExtender)
            => _attemptHeatmapExtender = attemptHeatmapExtender;

        public JsonResult Heatmap()
        {
            var heatmap = _attemptHeatmapExtender.Extend();
            return Json(heatmap);
        }
    }
}