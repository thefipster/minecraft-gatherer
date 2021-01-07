using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public JsonResult OutcomePeriods(Periods period)
        {
            var outcomeHistogram = _outcomeStats.Extend(period);
            return Json(outcomeHistogram);
        }

        [HttpGet("timings")]
        public IActionResult Timings()
        {
            var viewmodel = new StatsTimingsViewModel();

            var bestTimes = _bestTimings.Extend();

            if (bestTimes.ContainsKey(Sections.BlazeRod))
                bestTimes.Remove(Sections.BlazeRod);

            if (bestTimes.ContainsKey(Sections.Fortress))
                bestTimes.Remove(Sections.Fortress);

            viewmodel.FastestSections = bestTimes;

            return View(viewmodel);
        }

        [HttpGet("timings/{section}/{period}")]
        public JsonResult TimingsPeriods(Sections section, Periods period)
        {
            var timingsByPeriod = _timingStats.Extend(section, period);
            var sectionTimes = timingsByPeriod.ToDictionary(x => x.Key.Label, y => y.Value.Select(z => TimeSpan.FromMilliseconds(z.Value).TotalMinutes));
            return Json(sectionTimes);
        }

        [HttpGet("timings/{section}")]
        public JsonResult TimingsAll(Sections section)
        {
            var timings = _timingStats.Extend(section);
            var sectionTimes = timings.Select(x => x.Value);
            return Json(sectionTimes.Select(x => TimeSpan.FromMilliseconds(x).TotalMinutes));
        }
    }
}