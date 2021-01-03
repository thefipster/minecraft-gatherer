using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Meta.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("stats")]
    public class StatsController : Controller
    {
        private readonly IAttemptHeatmapExtender _attemptHeatmapExtender;
        private readonly IOutcomeFinder _outcomeFinder;

        public StatsController(
            IAttemptHeatmapExtender attemptHeatmapExtender,
            IOutcomeFinder outcomeFinder)
        {
            _attemptHeatmapExtender = attemptHeatmapExtender;
            _outcomeFinder = outcomeFinder;
        }

        [HttpGet("heatmap/attempts")]
        public JsonResult Heatmap()
        {
            var heatmap = _attemptHeatmapExtender.Extend();
            return Json(heatmap);
        }

        [HttpGet("outcome/{period}")]
        public JsonResult OutcomePeriods(Period period)
        {
            return Json(period);
        }
    }
}