using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("stats")]
    public class StatsController : Controller
    {
        private readonly IAttemptHeatmapExtender _attemptHeatmapExtender;
        private readonly IOutcomeStatsExtender _outcomeStats;

        public StatsController(
            IAttemptHeatmapExtender attemptHeatmapExtender,
            IOutcomeStatsExtender outcomeStats)
        {
            _attemptHeatmapExtender = attemptHeatmapExtender;
            _outcomeStats = outcomeStats;
        }

        [HttpGet("heatmap/attempts")]
        public JsonResult Heatmap()
        {
            var heatmap = _attemptHeatmapExtender.Extend();
            return Json(heatmap);
        }

        [HttpGet("outcomes")]
        public IActionResult Outcomes()
        {
            return View();
        }

        [HttpGet("outcome/relative")]
        public JsonResult OutcomeRelative()
        {
            var outcomeHistogram = _outcomeStats.Extend();
            return Json(outcomeHistogram);
        }

        [HttpGet("outcome/{period}")]
        public JsonResult OutcomePeriods(Period period)
        {
            var outcomeHistogram = _outcomeStats.Extend(period);
            return Json(outcomeHistogram);
        }
    }
}