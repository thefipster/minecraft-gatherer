using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Overview.Abstractions;

namespace TheFipster.Minecraft.Overview.Services
{
    public class JobPrioritizer : IJobPrioritizer
    {
        private readonly Dictionary<Outcomes, int> outcomePriorities = new Dictionary<Outcomes, int>
        {
            { Outcomes.Unknown, 1000 },
            { Outcomes.Untouched, 1000 },
            { Outcomes.Discarded, 2000 },
            { Outcomes.ResetSpawn, 3000 },
            { Outcomes.ResetNether, 4000 },
            { Outcomes.ResetFortress, 5000 },
            { Outcomes.ResetSearch, 6000 },
            { Outcomes.ResetStronghold, 7000 },
            { Outcomes.ResetEnd, 8000 },
            { Outcomes.Finished, 9000 }
        };

        private readonly IAnalyticsReader _analytics;

        public JobPrioritizer(IAnalyticsReader analytics)
        {
            _analytics = analytics;
        }

        public int Prioritize(string worldname)
        {
            var data = _analytics.Get(worldname);

            var priority = 0;

            priority += outcomePriorities[data.Outcome];
            priority += getDatePriority(data.World.CreatedOn);

            return priority;
        }

        private int getDatePriority(DateTime date)
            => (int)(DateTime.UtcNow - date).TotalHours;

    }
}
