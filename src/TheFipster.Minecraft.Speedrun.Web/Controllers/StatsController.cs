using Microsoft.AspNetCore.Mvc;
using System;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Route("stats")]
    public class StatsController : Controller
    {
        private readonly IAttemptHeatmapExtender _attemptHeatmapExtender;
        private readonly IOutcomeStatsExtender _outcomeStats;
        private readonly ITimingStatsExtender _timingStats;
        private readonly IBestTimingsExtender _bestTimings;

        public StatsController(
            IAttemptHeatmapExtender attemptHeatmapExtender,
            IOutcomeStatsExtender outcomeStats,
            ITimingStatsExtender timingStats,
            IBestTimingsExtender bestTimings)
        {
            _attemptHeatmapExtender = attemptHeatmapExtender;
            _outcomeStats = outcomeStats;
            _timingStats = timingStats;
            _bestTimings = bestTimings;
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

        [HttpGet("timings")]
        public IActionResult Timings()
        {
            var viewmodel = new StatsTimingsViewModel();

            var now = DateTime.UtcNow;

            var alltimeStats = _timingStats.Extend();
            var lastYear = _timingStats.Extend(now.AddDays(-360), now);
            var lastSemester = _timingStats.Extend(now.AddDays(-180), now);
            var lastQuarter = _timingStats.Extend(now.AddDays(-90), now);
            var lastMonth = _timingStats.Extend(now.AddDays(-30), now);
            var lastWeek = _timingStats.Extend(now.AddDays(-7), now);
            var lastDay = _timingStats.Extend(now.AddDays(-1), now);

            viewmodel.Stats.Add("all-time", alltimeStats);
            viewmodel.Stats.Add("last year", lastYear);
            viewmodel.Stats.Add("last semester", lastSemester);
            viewmodel.Stats.Add("last quarter", lastQuarter);
            viewmodel.Stats.Add("last month", lastMonth);
            viewmodel.Stats.Add("last week", lastWeek);
            viewmodel.Stats.Add("last 24 hours", lastDay);

            var bestTimes = _bestTimings.Extend();
            viewmodel.FastestSections = bestTimes;

            return View(viewmodel);
        }
    }
}