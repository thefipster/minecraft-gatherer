using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class AttemptHeatmapExtender : IAttemptHeatmapExtender
    {
        private readonly IOutcomeFinder _outcomeFinder;

        public AttemptHeatmapExtender(IOutcomeFinder outomeFinder)
            => _outcomeFinder = outomeFinder;

        public Dictionary<string, DayContent> Extend()
        {
            var start = DateTime.Now.Date.AddDays(-366);
            var end = DateTime.Now.Date;

            var outcomes = _outcomeFinder
                .Get(start, end)
                .GroupBy(x => x.Timestamp.Date);

            var result = createCategories(outcomes);
            return result;
        }

        private Dictionary<string, DayContent> createCategories(IEnumerable<IGrouping<DateTime, RunMeta<Outcomes>>> outcomes)
        {
            var attemptHistogram = new Dictionary<string, DayContent>();
            foreach (var day in outcomes)
            {
                var date = day.Key.ToString("yyyy-MM-dd");
                var attempts = day.Count();

                var content = new DayContent();

                if (attempts >= 50)
                    content.Items.Add("at least 50");
                else if (attempts >= 30)
                    content.Items.Add("at least 30");
                else if (attempts >= 20)
                    content.Items.Add("at least 20");
                else if (attempts >= 10)
                    content.Items.Add("at least 10");
                else if (attempts >= 5)
                    content.Items.Add("at least 5");
                else if (attempts >= 1)
                    content.Items.Add("at least 1");

                attemptHistogram.Add(date, content);
            }

            return attemptHistogram;
        }
    }
}
